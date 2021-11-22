using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("WorkTask");
        }
    }
}
