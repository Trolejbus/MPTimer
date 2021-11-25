using MPTimer.Models;

namespace MPTimer.Interfaces
{
    internal interface ISourceControlService
    {
        Task<IEnumerable<TraySourceControl>> GetWatched();
    }
}
