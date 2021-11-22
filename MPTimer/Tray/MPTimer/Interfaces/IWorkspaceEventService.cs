using MPTimer.Models;

namespace MPTimer.Interfaces
{
    internal interface IWorkspaceEventService
    {
        Task<TrayWorkspaceEvent> Create(TrayWorkspaceEvent workspaceEvent);
        Task<TrayWorkspaceEvent> Update(TrayWorkspaceEvent workspaceEvent);
    }
}
