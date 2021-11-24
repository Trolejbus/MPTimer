using Microsoft.Extensions.Configuration;
using MPTimer.Controls;
using MPTimer.Enums;
using MPTimer.Interfaces;
using MPTimer.Models;

namespace MPTimer.Controllers
{
    internal class WorkspaceEventsController : IWorkspaceEventsController
    {
        private readonly IWorkspaceEventService _service;
        private readonly Guid _agentId;
        private TrayWorkspaceEvent? workspaceEvent;

        public WorkspaceEventsController(IWorkspaceEventService service, IConfiguration configuration)
        {
            _service = service;
            _agentId = Guid.Parse(configuration["AgentId"]);
        }

        public async Task SessionLocked()
        {
            workspaceEvent = new TrayWorkspaceEvent(Guid.NewGuid(), TrayWorkspaceEventType.ScreenLocked, DateTime.UtcNow, _agentId);
            await _service.Create(workspaceEvent);
        }

        public async Task SessionUnlocked()
        {
            if (workspaceEvent == null)
            {
                return;
            }

            workspaceEvent.To = DateTime.UtcNow;

            var form = new GetTextForm()
            {
                Label = "What have you done during screen lock?",
                Hints = new[] { "Franek", "Kawa / Herbata / Siku", "Obiad" },
            };
            form.ShowDialog();
            workspaceEvent.Data = form.Value;

            await _service.Update(workspaceEvent);
        }
    }
}
