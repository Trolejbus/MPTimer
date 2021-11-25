using Microsoft.EntityFrameworkCore;
using MPSourceControl.Entities;

namespace MPSourceControl
{
    public class SourceControlContext : DbContext
    {

        public SourceControlContext(
            DbContextOptions<SourceControlContext> options)
            : base(options)
        {
        }

        public DbSet<SourceControl> SourceControl => Set<SourceControl>();

        public DbSet<SourceControlStatus> Status => Set<SourceControlStatus>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("SourceControl");
        }
    }
}
