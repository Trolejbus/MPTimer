using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using MPTimer.Interfaces;

namespace MPTimer
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            ApplicationConfiguration.Initialize();

            var container = new MPTimerContainerFactory().Build();
            using (var scope = container.BeginLifetimeScope())
            {
                var startableAsync = scope.Resolve<IEnumerable<IStartableAsync>>();
                foreach (var startable in startableAsync)
                {
                    await startable.StartAsync();
                }

                var configuration = scope.Resolve<IConfiguration>();
                var workspaceController = scope.Resolve<IWorkspaceEventsController>();
                var signalRController = scope.Resolve<ISignalRController>();

                _ = signalRController.Connect();
                SystemEvents.SessionSwitch += SystemEvents_SessionSwitch(workspaceController);
                Application.Run();
            }
        }

        private static SessionSwitchEventHandler SystemEvents_SessionSwitch(IWorkspaceEventsController workspaceEventsController)
        {
            return (object sender, SessionSwitchEventArgs e) =>
            {
                if (e.Reason == SessionSwitchReason.SessionLock)
                {
                    workspaceEventsController.SessionLocked();
                }
                else if (e.Reason == SessionSwitchReason.SessionUnlock)
                {
                    workspaceEventsController.SessionUnlocked();
                }
            };
        }
    }
}