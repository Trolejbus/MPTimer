﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MPTimerWorkTask.Interfaces;

namespace MPTimerWorkTask
{
    public static class AgentServices
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AgentContext>(options =>
                options.UseSqlServer("name=ConnectionStrings:Agent"));
            services.AddScoped<IAgentRepository, AgentRepository>();
        }
    }
}
