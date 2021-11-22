using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MPTimerAgent.Entities;
using MPTimerAgent.Interfaces;
using MPTimerAgent.Models;

namespace MPTimerWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsRuntimesController : Controller
    {
        private IAgentRuntimesRepository _repository;
        private readonly IMapper _mapper;

        public AgentsRuntimesController(IAgentRuntimesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("/api/agentRuntimes")]
        public async Task<IEnumerable<AgentRuntime>> GetAll()
        {
            return await _repository.GetAll(new AgentRuntimeFilter() { OnlyToday = true });
        }
    }
}
