using Microsoft.AspNetCore.SignalR;
using MPTimerAgent.Interfaces;

namespace MPTimerWeb.Hubs
{
    public class AgentHub : Hub
    {
        private static Dictionary<string, string> Agents = new Dictionary<string, string>();
        private readonly IAgentRuntimesRepository _repository;

        public AgentHub(IAgentRuntimesRepository repository)
        {
            _repository = repository;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            var httpContext = Context.GetHttpContext() ?? throw new Exception("Cannot be null");
            var agentId = httpContext.Request.Query["agentId"].ToString();
            if (!string.IsNullOrEmpty(agentId))
            {
                Agents.Add(Context.ConnectionId, agentId);
                await Groups.AddToGroupAsync(Context.ConnectionId, "Agents");
                await _repository.NotifyConnect(new Guid(agentId));
                await NotifyAgentConnected(agentId);
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Frontends");
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
            var agentId = Agents[Context.ConnectionId];
            if (!string.IsNullOrEmpty(agentId))
            {
                Agents.Remove(Context.ConnectionId);
                await _repository.NotifyDisconnect(new Guid(agentId));
                await NotifyAgentDisconnected(agentId);
            }
        }
        
        public async Task NotifyAgentConnected(string agentId)
        {
            await Clients.Group("Frontends").SendAsync("AgentConnected", agentId);
        }

        public async Task NotifyAgentDisconnected(string agentId)
        {
            await Clients.Group("Frontends").SendAsync("AgentDisconnected", agentId);
        }

        public async Task WorkspaceEventAdded(Guid workspaceId)
        {
        }

        public async Task WorkspaceEventUpdated(Guid workspaceId)
        {
            await Clients.Group("Frontends").SendAsync("WorkspaceEventUpdated", workspaceId);
        }
    }
}
