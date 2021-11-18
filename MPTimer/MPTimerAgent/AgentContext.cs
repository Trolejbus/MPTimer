using Microsoft.EntityFrameworkCore;
using MPTimerAgent.Models;

namespace MPTimerAgent
{
    public class AgentContext: DbContext
    {

        public AgentContext(
            DbContextOptions<AgentContext> options)
            : base(options)
        {
        }

        public DbSet<Agent> Agents => Set<Agent>();
    }
}
