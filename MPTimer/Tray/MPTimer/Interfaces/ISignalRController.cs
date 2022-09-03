using MPTimer.Enums;

namespace MPTimer.Interfaces
{
    internal interface ISignalRController
    {
        event Action<SignalRConnectionStatus>? StatusChanged;
        public SignalRConnectionStatus Status { get; }
        Task ConnectAsync();
    }
}
