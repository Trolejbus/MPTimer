using Microsoft.EntityFrameworkCore;
using MPTimerWorkspaceEvent.Models;

namespace MPTimerWorkspaceEvent
{
    public class WorkspaceEventContext: DbContext
    {

        public WorkspaceEventContext(
            DbContextOptions<WorkspaceEventContext> options)
            : base(options)
        {
        }

        public DbSet<WorkspaceEvent> WorkspaceEvents => Set<WorkspaceEvent>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("WorkspaceEvent");
        }
    }
}
