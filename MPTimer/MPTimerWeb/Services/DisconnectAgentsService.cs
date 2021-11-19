using MPTimerAgent.Interfaces;

namespace MPTimerWeb.Services
{
    public class DisconnectAgentsService : IHostedService
    {
        private IServiceProvider _services;

        public DisconnectAgentsService(IServiceProvider services)
        {
            _services = services;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _services.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IAgentRepository>();
            await repository.DisconnectAllAgents();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
