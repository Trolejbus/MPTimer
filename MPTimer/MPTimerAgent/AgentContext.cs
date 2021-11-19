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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.LogTo(message => Debug.WriteLine(message));

        public DbSet<Agent> Agent => Set<Agent>();
        public DbSet<AgentRuntime> AgentRuntime => Set<AgentRuntime>();
    }
}
