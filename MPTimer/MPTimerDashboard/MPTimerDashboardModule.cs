using Autofac;
using MPTimer.Components.Dashboard;

namespace MPTimerDashboard
{
    public class MPTimerDashboardModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DashboardController>().As<DashboardController>().As<IStartable>().SingleInstance();
        }
    }
}
