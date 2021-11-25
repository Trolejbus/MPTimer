using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MPSourceControl.Interfaces;

namespace MPSourceControl
{
    public static class SourceControlServices
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SourceControlContext>(options =>
                options.UseSqlServer("name=ConnectionStrings:SourceControl"));
            services.AddScoped<ISourceControlRepository, SourceControlRepository>();
        }
    }
}
