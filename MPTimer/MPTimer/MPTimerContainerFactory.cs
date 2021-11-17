using Autofac;
using MPTimerEventBus;
using MPTimerTime;
using MPTimerUtils;

namespace MPTimer
{
    public class MPTimerContainerFactory
    {
        public IContainer Build()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<MPTimerModule>();
            builder.RegisterModule<MPTimerUtilsModule>();
            builder.RegisterModule<MPTimerEventBusModule>();
            builder.RegisterModule<MPTimerTimeModule>();
            return builder.Build();
        }
    }
}
