using Autofac;
using MPTimerEventBus;
using MPTimerUtils.Service;

namespace MPTimer
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var container = new MPTimerContainerFactory().Build();
            using (var scope = container.BeginLifetimeScope())
            {
                ScopeSingleton.Scope = scope;
                var eventBus = scope.Resolve<EventBus>();
                eventBus.Application.InvokeLoad();
                
                Application.Run();
            }
        }
    }
}