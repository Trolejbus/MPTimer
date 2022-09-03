using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using MPTimer.Controls;
using MPTimer.Interfaces;
using MPTimer.Models;
using RestSharp;

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
            try
            {
                ApplicationConfiguration.Initialize();
                var container = new MPTimerContainerFactory().Build();
                using (var scope = container.BeginLifetimeScope())
                {
                    var startableAsync = scope.Resolve<IEnumerable<IStartableAsync>>();
                    foreach (var startable in startableAsync)
                    {
                        await startable.StartAsync().ConfigureAwait(false);
                    }

                    var configuration = scope.Resolve<IConfiguration>();
                    var workspaceController = scope.Resolve<IWorkspaceEventsController>();
                    var signalRController = scope.Resolve<ISignalRController>();

                    _ = signalRController.ConnectAsync().ConfigureAwait(false);
                    SystemEvents.SessionSwitch += SystemEvents_SessionSwitch(workspaceController);
                    Application.Run();
                }
            }
            catch(Exception ex)
            {

            }
        }

        private static SessionSwitchEventHandler SystemEvents_SessionSwitch(IWorkspaceEventsController workspaceEventsController)
        {
            return (object sender, SessionSwitchEventArgs e) =>
            {
                if (e.Reason == SessionSwitchReason.SessionLock)
                {
                    workspaceEventsController.SessionLocked().ConfigureAwait(false);
                }
                else if (e.Reason == SessionSwitchReason.SessionUnlock)
                {
                    workspaceEventsController.SessionUnlocked().ConfigureAwait(false);
                }
            };
        }
    }
}