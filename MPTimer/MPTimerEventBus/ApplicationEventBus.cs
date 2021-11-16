namespace MPTimerEventBus
{
    public class ApplicationEventBus
    {
        public event Action? OnOpenDashboard;
        public void InvokeOpenDashboard() => OnOpenDashboard?.Invoke();
        public event Action? OnLoad;
        public void InvokeLoad() => OnLoad?.Invoke();
    }
}
