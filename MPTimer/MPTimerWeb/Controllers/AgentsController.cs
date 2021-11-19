using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MPTimerAgent.Entities;
using MPTimerAgent.Interfaces;
using MPTimerWeb.Dtos.Agent;

namespace MPTimerWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : Controller
    {
        private IAgentRepository _repository;
        private readonly IMapper _mapper;

        public AgentsController(IAgentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("/api/agent/{id}")]
        public async Task<AgentDto> GetById(Guid id)
        {
            var checks = await _repository.GetById(id);
            return _mapper.Map<AgentDto>(checks);
        }

        [HttpGet("/api/agents")]
        public async Task<IEnumerable<AgentDto>> GetAll()
        {
            var checks = await _repository.GetAll();
            return _mapper.Map<IEnumerable<AgentDto>>(checks);
        }

        [HttpPost("/api/agent")]
        public async Task<AgentDto> Add(AgentDto AgentDto)
        {
            var model = _mapper.Map<Agent>(AgentDto);
            var checks = await _repository.Add(model);
            return _mapper.Map<AgentDto>(checks);
        }

        [HttpPut("/api/agent/{id}")]
        public async Task<AgentDto> Add(Guid id, AgentDto AgentDto)
        {
            var model = _mapper.Map<Agent>(AgentDto);
            var checks = await _repository.Update(id, model);
            return _mapper.Map<AgentDto>(checks);
        }

        [HttpDelete("/api/agent/{id}")]
        public async Task Remove(Guid id)
        {
            await _repository.Remove(id);
        }

        [HttpGet("/api/agents/status")]
        public async Task<IEnumerable<AgentStateDto>> GetAgentStatuses()
        {
            var statuses = await _repository.GetAgentStates();
            return _mapper.Map<IEnumerable<AgentStateDto>>(statuses);
        }
    }
}
