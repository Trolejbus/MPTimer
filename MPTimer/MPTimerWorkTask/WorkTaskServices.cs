using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MPTimerWorkTask.Interfaces;

namespace MPTimerWorkTask
{
    public static class WorkTaskServices
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WorkTaskContext>(options =>
                options.UseSqlServer("name=ConnectionStrings:WorkTask"));
            services.AddScoped<IWorkTaskRepository, WorkTaskRepository>();
        }
    }
}
