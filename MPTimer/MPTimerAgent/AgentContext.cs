using Microsoft.EntityFrameworkCore;
using MPTimerAgent.Entities;
using System.Diagnostics;

namespace MPTimerAgent
{
    public class AgentContext: DbContext
    {
        public AgentContext(
            DbContextOptions<AgentContext> options)
            : base(options)
        {

        }

        public DbSet<Agent> Agent => Set<Agent>();
        public DbSet<AgentRuntime> AgentRuntime => Set<AgentRuntime>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Agent");
            modelBuilder
                .Entity<AgentRuntime>()
                .Property(e => e.From)
                .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            modelBuilder
                .Entity<AgentRuntime>()
                .Property(e => e.To)
                .HasConversion(v => v, v => v != null ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : null);
        }
    }
}
