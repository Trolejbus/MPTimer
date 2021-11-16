using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MPTimerWorkTask.Models;

namespace MPTimerWorkTask
{
    public class WorkTaskContext: DbContext
    {

        public WorkTaskContext(
            DbContextOptions<WorkTaskContext> options)
            : base(options)
        {
        }

        public DbSet<WorkTask> WorkTasks => Set<WorkTask>();
    }
}
