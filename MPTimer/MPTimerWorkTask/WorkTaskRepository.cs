using Microsoft.EntityFrameworkCore;
using MPTimerWorkTask.Interfaces;
using MPTimerWorkTask.Models;

namespace MPTimerWorkTask
{
    internal class WorkTaskRepository : IWorkTaskRepository
    {
        private readonly WorkTaskContext _context;

        public WorkTaskRepository(WorkTaskContext context)
        {
            _context = context;
        }

        public async Task<WorkTask> GetById(Guid id)
        {
            var model = await _context.WorkTasks.FirstAsync(t => t.Id == id);
            return model;
        }

        public async Task<IEnumerable<WorkTask>> GetAll()
        {
            var workTasks = await _context.WorkTasks.ToListAsync();
            return workTasks;
        }

        public async Task<WorkTask> Add(WorkTask task)
        {
            var entity = _context.WorkTasks.Add(task);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<WorkTask> Update(Guid id, WorkTask model)
        {
            var entity = await _context.WorkTasks.FirstAsync(t => t.Id == id);
            _context.Entry(entity).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Remove(Guid id)
        {
            var model = await _context.WorkTasks.FirstAsync(t => t.Id == id);
            _context.WorkTasks.Remove(model);
            await _context.SaveChangesAsync();
        }
    }
}
