using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using MPTimer.Enums;
using MPTimer.Interfaces;

namespace MPTimer.Controllers
{
    internal class SignalRController : ISignalRController
    {
        public event Action<SignalRConnectionStatus>? StatusChanged;
        public SignalRConnectionStatus Status { get; private set; }

        private readonly IConfiguration _configuration;
        private readonly HubConnection connection;


        public SignalRController(IConfiguration configuration)
        {
            _configuration = configuration;
            string agentId = _configuration["AgentId"] ?? throw new Exception("Missing AgentId");

            connection = new HubConnectionBuilder()
                .WithUrl($"{_configuration["BackendUrl"]}/Agent?agentId={agentId}", options =>
                {
                    options.AccessTokenProvider = () => {
                        var task = Task.FromResult<string?>(agentId);
                        task.ConfigureAwait(false);
                        return task;
                    };
                })
                .Build();
            ChangeStatus(SignalRConnectionStatus.NotConnected);
            connection.Closed += async (error) =>
            {
                ChangeStatus(SignalRConnectionStatus.Disconnected);
                await Task.Delay(new Random().Next(0, 5) * 1000).ConfigureAwait(false);
                await ConnectAsync().ConfigureAwait(false);
            };
        }

        public async Task ConnectAsync()
        {
            ChangeStatus(SignalRConnectionStatus.Connecting);
            await connection.StartAsync().ConfigureAwait(false);
            ChangeStatus(SignalRConnectionStatus.Connected);
        }
        
        private void ChangeStatus(SignalRConnectionStatus status)
        {
            Status = status;
            StatusChanged?.Invoke(status);
        }
    }
}
