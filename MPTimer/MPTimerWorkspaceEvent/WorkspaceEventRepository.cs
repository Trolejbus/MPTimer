using Microsoft.EntityFrameworkCore;
using MPTimerWorkspaceEvent.Entities;
using MPTimerWorkspaceEvent.Interfaces;
using MPTimerWorkspaceEvent.Models;

namespace MPTimerWorkspaceEvent
{
    internal class WorkspaceEventRepository : IWorkspaceEventRepository
    {
        private readonly WorkspaceEventContext _context;

        public WorkspaceEventRepository(WorkspaceEventContext context)
        {
            _context = context;
        }

        public async Task<WorkspaceEvent> GetById(Guid id)
        {
            var model = await _context.WorkspaceEvents.FirstAsync(t => t.Id == id);
            return model;
        }

        public async Task<IEnumerable<WorkspaceEvent>> GetAll(WorkspaceEventFilter? filter = null)
        {
            var query = _context.WorkspaceEvents.AsQueryable();
            if (filter?.OnlyToday ?? false)
            {
                query = query.Where(q => q.From > DateTime.UtcNow.Date && (q.To == null || q.To < DateTime.UtcNow.Date.AddDays(1)));
            }

            return await query.ToListAsync();
        }

        public async Task<WorkspaceEvent> Add(WorkspaceEvent task)
        {
            var entity = _context.WorkspaceEvents.Add(task);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<WorkspaceEvent> Update(Guid id, WorkspaceEvent model)
        {
            var entity = await _context.WorkspaceEvents.FirstAsync(t => t.Id == id);
            _context.Entry(entity).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Remove(Guid id)
        {
            var model = await _context.WorkspaceEvents.FirstAsync(t => t.Id == id);
            _context.WorkspaceEvents.Remove(model);
            await _context.SaveChangesAsync();
        }
    }
}
