using Autofac;
using MPTimerEventBus;
using MPTimerTime.Interfaces;
using System.Timers;

namespace MPTimerTime.Controller
{
    internal class TimerController : ITimerController, IStartable
    {
        private EventBus _eventBus;
        private System.Timers.Timer timer;

        public TimerController(EventBus eventBus)
        {
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = false;
            _eventBus = eventBus;
        }

        public void Start()
        {
            timer.Enabled = true;
        }

        private void OnTimedEvent(object? sender, ElapsedEventArgs e)
        {
            _eventBus.Timer.InvokeTick();
        }
    }
}
