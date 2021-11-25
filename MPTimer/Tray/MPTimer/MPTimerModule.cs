using Autofac;
using Microsoft.Extensions.Configuration;
using MPTimer.Components;
using MPTimer.Controllers;
using MPTimer.Interfaces;
using MPTimer.Service;

namespace MPTimer
{
    internal class MPTimerModule : Module
    {
        public MPTimerModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            var confBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = confBuilder.Build();

            builder.RegisterType<TrayController>().As<TrayController>().As<IStartable>().SingleInstance();
            builder.RegisterType<TimerController>().As<ITimerController>().As<IStartable>().SingleInstance();
            builder.RegisterType<WorkspaceEventsController>().As<IWorkspaceEventsController>().SingleInstance();
            builder.RegisterType<SignalRController>().As<ISignalRController>().SingleInstance();
            builder.RegisterType<WorkspaceEventService>().As<IWorkspaceEventService>().SingleInstance();
            builder.RegisterType<SourceControlController>().As<ISourceControlController>().As<IStartableAsync>().SingleInstance();
            builder.RegisterType<SourceControlService>().As<ISourceControlService>().SingleInstance();
            builder.RegisterInstance(configuration).As<IConfiguration>().SingleInstance();
        }
    }
}
