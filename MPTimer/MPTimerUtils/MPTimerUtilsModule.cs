using Autofac;
using MPTimerUtils.Interfaces;
using MPTimerUtils.Service;

namespace MPTimerUtils
{
    public class MPTimerUtilsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RealTimeService>().As<ITimeService>().SingleInstance();
        }
    }
}
