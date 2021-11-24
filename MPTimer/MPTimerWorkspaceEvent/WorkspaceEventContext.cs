using Microsoft.EntityFrameworkCore;
using MPTimerWorkspaceEvent.Entities;

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
            modelBuilder
                .Entity<WorkspaceEvent>()
                .Property(e => e.From)
                .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            modelBuilder
                .Entity<WorkspaceEvent>()
                .Property(e => e.To)
                .HasConversion(v => v, v => v != null ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : null);
        }
    }
}
