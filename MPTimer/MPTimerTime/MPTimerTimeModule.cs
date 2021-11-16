using Autofac;
using MPTimerTime.Controller;
using MPTimerTime.Interfaces;

namespace MPTimerTime
{
    public class MPTimerTimeModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TimerController>().As<ITimerController>().As<IStartable>().SingleInstance();
        }
    }
}
