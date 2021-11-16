namespace MPTimerEventBus
{
    public class TimerEventBus
    {
        public event Action? OnTick;
        public void InvokeTick() => OnTick?.Invoke();
    }
}
