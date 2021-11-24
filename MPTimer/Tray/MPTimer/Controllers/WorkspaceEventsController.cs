using Microsoft.Extensions.Configuration;
using MPTimer.Controls;
using MPTimer.Enums;
using MPTimer.Interfaces;
using MPTimer.Models;
using MPTimer.Tools;

namespace MPTimer.Controllers
{
    internal class WorkspaceEventsController : IWorkspaceEventsController
    {
        private readonly IWorkspaceEventService _service;
        private readonly Guid _agentId;
        private readonly int _idleTime;
        private TrayWorkspaceEvent? workspaceEventSessionLocked;
        private TrayWorkspaceEvent? workspaceEventIdleTime;
        private uint previousIdleTime = uint.MaxValue;

        public WorkspaceEventsController(IWorkspaceEventService service, IConfiguration configuration, ITimerController timerController)
        {
            _service = service;
            _agentId = Guid.Parse(configuration["AgentId"]);
            _idleTime = int.Parse(configuration["IdleTime"]);
            timerController.OnTick += TimerController_OnTick;
        }

        public async Task SessionLocked()
        {
            workspaceEventSessionLocked = new TrayWorkspaceEvent(Guid.NewGuid(), TrayWorkspaceEventType.ScreenLocked, DateTime.UtcNow, _agentId);
            await _service.Create(workspaceEventSessionLocked);
        }

        private void TimerController_OnTick()
        {
            var idleTime = IdleTimeUtils.GetIdleTime();
            if (idleTime < previousIdleTime)
            {
                if (workspaceEventIdleTime != null)
                {
                    IdleStop().Wait();
                }

                var idleFrom = DateTime.UtcNow;
                workspaceEventIdleTime = new TrayWorkspaceEvent(Guid.NewGuid(), TrayWorkspaceEventType.IdleTime, idleFrom, _agentId);
            }

            previousIdleTime = idleTime;
            if (idleTime < _idleTime)
            {
                return;
            }

            IdleStart(idleTime).Wait();
        }

        public async Task SessionUnlocked()
        {
            if (workspaceEventSessionLocked == null)
            {
                return;
            }

            workspaceEventSessionLocked.To = DateTime.UtcNow;

            var form = new GetTextForm()
            {
                Label = "What have you done during screen lock?",
                Hints = new[] { "Franek", "Kawa / Herbata / Siku", "Obiad" },
            };
            form.ShowDialog();
            workspaceEventSessionLocked.Data = form.Value;

            await _service.Update(workspaceEventSessionLocked);
        }

        private async Task IdleStart(uint idleTime)
        {
            
            await _service.Create(workspaceEventIdleTime);
        }

        private async Task IdleStop()
        {
            if (workspaceEventIdleTime == null)
            {
                throw new Exception("workspaceEventSessionLocked cannot be null");
            }

            workspaceEventIdleTime.To = DateTime.UtcNow;
            await _service.Update(workspaceEventIdleTime);
        }
    }
}
