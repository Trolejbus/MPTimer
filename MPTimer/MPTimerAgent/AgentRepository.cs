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

        public async Task NotifyConnect(Guid id)
        {
            var agent = await _context.Agent.Include(a => a.AgentRuntimes).FirstAsync(a => a.Id == id);
            if (agent.AgentRuntimes.Any(r => r.To == null))
            {
                return;
            }

            agent.AgentRuntimes.Add(new AgentRuntime(id, DateTime.Now));
            await _context.SaveChangesAsync();
        }

        public async Task NotifyDisconnect(Guid id)
        {
            var agent = await _context.Agent.Include(a => a.AgentRuntimes).FirstAsync(a => a.Id == id);
            var runtime = agent.AgentRuntimes.FirstOrDefault(r => r.To == null);
            if (runtime == null) return;

            runtime.To = DateTime.Now;
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

        public async Task DisconnectAllAgents()
        {
            var agentRuntimes = await _context.AgentRuntime.ToListAsync();
            foreach (var agentRuntime in agentRuntimes)
            {
                agentRuntime.To = DateTime.Now;
            }

            await _context.SaveChangesAsync();
        }
    }
}
