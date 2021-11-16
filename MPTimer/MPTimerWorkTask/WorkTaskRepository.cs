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

        public async Task<IEnumerable<WorkTask>> GetAll()
        {
            var workTasks = await _context.WorkTasks.ToListAsync();
            return workTasks;
        }
    }
}
