using MPTimerWorkspaceEvent.Entities;
using MPTimerWorkspaceEvent.Models;

namespace MPTimerWorkspaceEvent.Interfaces
{
    public interface IWorkspaceEventRepository
    {
        Task<IEnumerable<WorkspaceEvent>> GetAll(WorkspaceEventFilter filter);
        Task<WorkspaceEvent> Add(WorkspaceEvent task);
        Task Remove(Guid id);
        Task<WorkspaceEvent> GetById(Guid id);
        Task<WorkspaceEvent> Update(Guid id, WorkspaceEvent model);
        Task TerminateAllAgentsEvents(Guid agentId);
    }
}
