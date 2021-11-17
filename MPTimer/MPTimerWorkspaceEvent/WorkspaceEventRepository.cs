using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<WorkspaceEvent>> GetAll()
        {
            var workTasks = await _context.WorkspaceEvents.ToListAsync();
            return workTasks;
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
