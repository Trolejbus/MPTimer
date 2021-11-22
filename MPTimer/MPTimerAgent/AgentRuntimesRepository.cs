﻿using Microsoft.EntityFrameworkCore;
using MPTimerAgent.Entities;
using MPTimerAgent.Interfaces;
using MPTimerAgent.Models;

namespace MPTimerAgent
{
    public class AgentRuntimesRepository : IAgentRuntimesRepository
    {
        private readonly AgentContext _context;

        public AgentRuntimesRepository(AgentContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AgentRuntime>> GetAll(AgentRuntimeFilter? filter)
        {
            var query = _context.AgentRuntime.AsQueryable();
            if (filter?.OnlyToday ?? false)
            {
                query = query.Where(q => q.From > DateTime.UtcNow.Date && (q.To == null || q.To < DateTime.UtcNow.Date.AddDays(1)));
            }

            return await query.ToListAsync();
        }

        public async Task NotifyConnect(Guid id)
        {
            var agent = await _context.Agent.Include(a => a.AgentRuntimes).FirstAsync(a => a.Id == id);
            if (agent.AgentRuntimes.Any(r => r.To == null))
            {
                throw new Exception("Agent is already connected");
            }

            agent.AgentRuntimes.Add(new AgentRuntime(id, DateTime.UtcNow));
            await _context.SaveChangesAsync();
        }

        public async Task NotifyDisconnect(Guid id)
        {
            var agent = await _context.Agent.Include(a => a.AgentRuntimes).FirstAsync(a => a.Id == id);
            var runtime = agent.AgentRuntimes.FirstOrDefault(r => r.To == null);
            if (runtime == null) return;

            runtime.To = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task DisconnectAllAgents()
        {
            var agentRuntimes = await _context.AgentRuntime.ToListAsync();
            foreach (var agentRuntime in agentRuntimes)
            {
                agentRuntime.To = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }
    }
}
