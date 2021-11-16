using Autofac;

namespace MPTimerEventBus
{
    public class EventBus
    {
        public EventBus()
        {
        }

        public ApplicationEventBus Application { get; } = new ApplicationEventBus();
        public TimerEventBus Timer { get; } = new TimerEventBus();
    }
}
