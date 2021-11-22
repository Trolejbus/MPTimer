using MPTimerAgent.Entities;
using MPTimerAgent.Models;

namespace MPTimerAgent.Interfaces
{
    public interface IAgentRuntimesRepository
    {
        Task DisconnectAllAgents();
        Task<IEnumerable<AgentRuntime>> GetAll(AgentRuntimeFilter? filter);
        Task NotifyConnect(Guid id);
        Task NotifyDisconnect(Guid id);
    }
}