using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MPTimerWorkTask.Models;

namespace MPTimerWorkTask
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
