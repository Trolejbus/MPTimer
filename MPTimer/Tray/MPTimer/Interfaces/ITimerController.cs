namespace MPTimer.Interfaces
{
    public interface ITimerController
    {
        event Action? OnTick;
    }
}
