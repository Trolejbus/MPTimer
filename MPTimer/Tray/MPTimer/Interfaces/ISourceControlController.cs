using MPTimer.Models;

namespace MPTimer.Interfaces
{
    internal interface ISourceControlController
    {
        event Action<IEnumerable<TraySourceControl>>? SourceControlLoaded;
        event Action<IEnumerable<TraySourceControlStatus>>? SourceControlStatusUpdated;
    }
}
