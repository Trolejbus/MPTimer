using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MPTimerAgent.Entities;
using MPTimerAgent.Enums;
using MPTimerAgent.Interfaces;
using MPTimerAgent.Models;

namespace MPTimerAgent
{
    internal class AgentRepository : IAgentRepository
    {
        private readonly AgentContext _context;

        public AgentRepository(AgentContext context)
        {
            _context = context;
        }

        public async Task<Agent> GetById(Guid id)
        {
            var model = await _context.Agent.FirstAsync(t => t.Id == id);
            return model;
        }

        public async Task<IEnumerable<Agent>> GetAll()
        {
            var entities = await _context.Agent.ToListAsync();
            return entities;
        }

        public async Task<Agent> Add(Agent task)
        {
            var entity = _context.Agent.Add(task);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<Agent> Update(Guid id, Agent model)
        {
            var entity = await _context.Agent.FirstAsync(t => t.Id == id);
            _context.Entry(entity).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Remove(Guid id)
        {
            var model = await _context.Agent.FirstAsync(t => t.Id == id);
            _context.Agent.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AgentState>> GetAgentStates()
        {
            var agents = await _context.Agent.ToListAsync();
            var agentsRuntimes = await _context.AgentRuntime
                .GroupBy(k => k.AgentId)
                .Select(g => new { AgentId = g.Key, Runtime = g.FirstOrDefault(f => f.To == null) })
                .ToListAsync();
            var onlineAgents = agentsRuntimes.Where(g => g.Runtime != null);
            return agents.Select(agent => new AgentState()
            {
                AgentId = agent.Id,
                Status = onlineAgents.Any(o => o.AgentId == agent.Id) ? AgentStatus.Online : AgentStatus.Offline,
            });
        }
    }
}
