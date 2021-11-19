using MPTimer.Interfaces;

namespace MPTimer.Controllers
{
    internal class WorkspaceEventsController : IWorkspaceEventsController
    {
        public async Task SessionLocked()
        {
            await Task.CompletedTask;
        }

        public async Task SessionUnlocked()
        {
            await Task.CompletedTask;
        }
    }
}
