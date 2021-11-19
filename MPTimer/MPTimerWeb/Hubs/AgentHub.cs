using Microsoft.AspNetCore.SignalR;
using MPTimerAgent.Interfaces;

namespace MPTimerWeb.Hubs
{
    public class AgentHub : Hub
    {
        private static Dictionary<string, string> Agents = new Dictionary<string, string>();
        private readonly IAgentRepository _repository;

        public AgentHub(IAgentRepository repository)
        {
            _repository = repository;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            var httpContext = Context.GetHttpContext() ?? throw new Exception("Cannot be null");
            var agentId = httpContext.Request.Query["agentId"];
            Agents.Add(Context.ConnectionId, agentId);
            await _repository.NotifyConnect(new Guid(agentId));
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
            var agentId = Agents[Context.ConnectionId];
            Agents.Remove(Context.ConnectionId);
            await _repository.NotifyDisconnect(new Guid(agentId));
        }

        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
