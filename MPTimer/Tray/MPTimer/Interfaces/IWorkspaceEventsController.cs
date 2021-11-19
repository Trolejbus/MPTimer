namespace MPTimer.Interfaces
{
    internal interface IWorkspaceEventsController
    {
        Task SessionLocked();
        Task SessionUnlocked();
    }
}
