using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MPTimerWorkspaceEvent.Interfaces;

namespace MPTimerWorkspaceEvent
{
    public static class WorkspaceEventServices
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WorkspaceEventContext>(options =>
                options.UseSqlServer("name=ConnectionStrings:WorkspaceEvent"));
            services.AddScoped<IWorkspaceEventRepository, WorkspaceEventRepository>();
        }
    }
}
