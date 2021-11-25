using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MPSourceControl.Entities;
using MPSourceControl.Interfaces;
using MPTimerWeb.Hubs;

namespace MPTimerWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SourceControlController : Controller
    {
        private ISourceControlRepository _repository;
        private readonly IHubContext<AgentHub> _hub;

        public SourceControlController(ISourceControlRepository repository, IHubContext<AgentHub> hub)
        {
            _repository = repository;
            _hub = hub;
        }

        [HttpGet("/api/sourceControl/{id}")]
        public async Task<SourceControl> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }

        [HttpGet("/api/sourceControls")]
        public async Task<IEnumerable<SourceControl>> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpPost("/api/sourceControl")]
        public async Task<SourceControl> Add(SourceControl model)
        {
            return await _repository.Add(model);
        }

        [HttpPut("/api/sourceControl/{id}")]
        public async Task<SourceControl> Update(Guid id, SourceControl model)
        {
            return await _repository.Update(id, model);
        }

        [HttpDelete("/api/sourceControl/{id}")]
        public async Task Remove(Guid id)
        {
            await _repository.Remove(id);
        }

        [HttpPut("/api/sourceControl/{id}/changeBranch/{branch}")]
        public async Task<SourceControl> ChangeBranch([FromRoute] Guid id, [FromRoute] string branch)
        {
            var result = await _repository.ChangeBranch(id, branch);
            await _hub.Clients.Group("Frontends").SendAsync("SourceControlBranchChanged", id);
            return result;
        }
    }
}
