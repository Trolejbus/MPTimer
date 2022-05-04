using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MPSourceControl.Entities;
using MPSourceControl.Interfaces;
using MPSourceControl.Models;
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
        public async Task<IEnumerable<SourceControl>> GetAll([FromQuery] SourceControlFilter filter)
        {
            filter.From = filter.From ?? DateTime.UtcNow;
            filter.To = filter.To ?? DateTime.UtcNow.AddDays(1);
            return await _repository.GetAll(filter);
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

        [HttpPut("/api/sourceControl/{id}/changeBranch")]
        public async Task<SourceControl> ChangeBranch([FromRoute] Guid id, [FromQuery] string branchName)
        {
            var result = await _repository.ChangeBranch(id, branchName);
            await _hub.Clients.Group("Frontends").SendAsync("SourceControlBranchChanged", id);
            return result;
        }
    }
}
