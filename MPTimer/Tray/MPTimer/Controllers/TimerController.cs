using Autofac;
using MPTimer.Interfaces;

namespace MPTimer.Controllers
{
    public class TimerController : ITimerController, IStartable
    {
        private System.Timers.Timer timer;
        public event Action? OnTick;

        public TimerController()
        {
            timer = new System.Timers.Timer(1000)
            {
                AutoReset = true
            };
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            OnTick?.Invoke();
        }

        public void Start()
        {
            timer.Start();
        }
    }
}
