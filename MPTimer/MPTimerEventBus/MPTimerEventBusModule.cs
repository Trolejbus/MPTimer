using Autofac;

namespace MPTimerEventBus
{
    public class MPTimerEventBusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventBus>().As<EventBus>().SingleInstance();
        }
    }
}
