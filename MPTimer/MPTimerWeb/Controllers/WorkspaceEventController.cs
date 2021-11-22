using Microsoft.AspNetCore.Mvc;
using MPTimerWorkspaceEvent.Interfaces;
using MPTimerWorkspaceEvent.Models;

namespace MPTimerWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkspaceEventController : Controller
    {
        private IWorkspaceEventRepository _repository;

        public WorkspaceEventController(IWorkspaceEventRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("/api/workspaceEvents")]
        public async Task<IEnumerable<WorkspaceEvent>> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpPost("/api/agent")]
        public async Task<WorkspaceEvent> Add(WorkspaceEvent workspaceEvent)
        {
            return await _repository.Add(workspaceEvent);
        }
    }
}
