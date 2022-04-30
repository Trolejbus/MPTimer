using Microsoft.AspNetCore.SignalR;
using MPSourceControl.Interfaces;
using MPTimerAgent.Interfaces;
using MPTimerWorkspaceEvent.Interfaces;

namespace MPTimerWeb.Hubs
{
    public class AgentHub : Hub
    {
        private static Dictionary<string, string> Agents = new Dictionary<string, string>();
        private readonly IAgentRuntimesRepository _repository;
        private readonly IWorkspaceEventRepository _workspaceEventRepository;
        private readonly ISourceControlRepository _sourceControlRepository;

        public AgentHub(
            IAgentRuntimesRepository repository,
            IWorkspaceEventRepository workspaceEventRepository,
            ISourceControlRepository sourceControlRepository)
        {
            _repository = repository;
            _workspaceEventRepository = workspaceEventRepository;
            _sourceControlRepository = sourceControlRepository;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            var httpContext = Context.GetHttpContext() ?? throw new Exception("Cannot be null");
            var agentId = httpContext.Request.Query["agentId"].ToString();
            if (!string.IsNullOrEmpty(agentId))
            {
                if (Agents.TryGetValue(agentId, out _))
                {
                    throw new Exception("Agent is already connected");
                }

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
                var agentIdGuid = new Guid(agentId);
                await _repository.NotifyDisconnect(agentIdGuid);
                await _workspaceEventRepository.TerminateAllAgentsEvents(agentIdGuid);
                await _sourceControlRepository.TerminateAllSourceControls(agentIdGuid);
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
    }
}
