using Autofac;

namespace MPTimer
{
    public class MPTimerContainerFactory
    {
        public IContainer Build()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<MPTimerModule>();
            return builder.Build();
        }
    }
}
