using Microsoft.AspNetCore.SignalR;
using MPSourceControl.Interfaces;
using MPTimerAgent.Interfaces;
using MPTimerWorkspaceEvent.Interfaces;

namespace MPTimerWeb.Hubs
{
    public class AgentHub : Hub
    {
        private static Dictionary<string, string> Agents = new Dictionary<string, string>();
        private static Dictionary<string, string> ChromeAgents = new Dictionary<string, string>();
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
            var type = httpContext.Request.Query["type"].ToString();
            if (type == "chromeWidget")
            {
                if (ChromeAgents.TryGetValue(agentId, out _))
                {
                    throw new Exception("Agent is already connected");
                }

                ChromeAgents.Add(Context.ConnectionId, agentId);
                await Groups.AddToGroupAsync(Context.ConnectionId, "ChromeAgents");
                await _repository.NotifyConnect(new Guid(agentId));
                await NotifyAgentConnected(agentId);
            }
            else if (!string.IsNullOrEmpty(agentId))
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
            var chromeAgentId = ChromeAgents[Context.ConnectionId];
            if (!string.IsNullOrEmpty(agentId) || !string.IsNullOrEmpty(chromeAgentId))
            {
                if (!string.IsNullOrEmpty(agentId))
                {
                    Agents.Remove(Context.ConnectionId);
                }

                if (!string.IsNullOrEmpty(chromeAgentId))
                {
                    ChromeAgents.Remove(Context.ConnectionId);
                }

                var realAgentId = string.IsNullOrEmpty(agentId) ? chromeAgentId : agentId;
                var agentIdGuid = new Guid(realAgentId);
                await _repository.NotifyDisconnect(agentIdGuid);
                await _workspaceEventRepository.TerminateAllAgentsEvents(agentIdGuid);
                await _sourceControlRepository.TerminateAllSourceControls(agentIdGuid);
                await NotifyAgentDisconnected(realAgentId);
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
