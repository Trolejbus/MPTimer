﻿using Microsoft.EntityFrameworkCore;
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
            var model = await _context.Agents.FirstAsync(t => t.Id == id);
            return model;
        }

        public async Task<IEnumerable<Agent>> GetAll()
        {
            var entities = await _context.Agents.ToListAsync();
            return entities;
        }

        public async Task<Agent> Add(Agent task)
        {
            var entity = _context.Agents.Add(task);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<Agent> Update(Guid id, Agent model)
        {
            var entity = await _context.Agents.FirstAsync(t => t.Id == id);
            _context.Entry(entity).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Remove(Guid id)
        {
            var model = await _context.Agents.FirstAsync(t => t.Id == id);
            _context.Agents.Remove(model);
            await _context.SaveChangesAsync();
        }
    }
}
