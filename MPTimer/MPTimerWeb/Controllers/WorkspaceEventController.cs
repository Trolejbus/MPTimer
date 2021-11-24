using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MPTimerWeb.Hubs;
using MPTimerWorkspaceEvent.Entities;
using MPTimerWorkspaceEvent.Interfaces;
using MPTimerWorkspaceEvent.Models;

namespace MPTimerWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkspaceEventController : Controller
    {
        private IWorkspaceEventRepository _repository;
        private readonly IHubContext<AgentHub> _hub;

        public WorkspaceEventController(IWorkspaceEventRepository repository, IHubContext<AgentHub> hub)
        {
            _repository = repository;
            _hub = hub;
        }

        [HttpGet("/api/workspaceEvents")]
        public async Task<IEnumerable<WorkspaceEvent>> GetAll()
        {
            return await _repository.GetAll(new WorkspaceEventFilter() { OnlyToday = true });
        }

        [HttpPost("/api/workspaceEvent")]
        public async Task<WorkspaceEvent> Add(WorkspaceEvent workspaceEvent)
        {
            var result = await _repository.Add(workspaceEvent);
            await _hub.Clients.Group("Frontends").SendAsync("WorkspaceEventAdded", workspaceEvent.Id);
            return result;
        }

        [HttpPut("/api/workspaceEvent/{id}")]
        public async Task<WorkspaceEvent> Update(Guid id, WorkspaceEvent workspaceEvent)
        {
            var result = await _repository.Update(id, workspaceEvent);
            await _hub.Clients.Group("Frontends").SendAsync("WorkspaceEventUpdated", id);
            return result;
        }
    }
}
