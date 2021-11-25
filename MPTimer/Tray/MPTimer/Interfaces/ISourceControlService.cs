using MPTimer.Models;

namespace MPTimer.Interfaces
{
    internal interface ISourceControlService
    {
        Task<IEnumerable<TraySourceControl>> GetWatched();
        void ChangeBranch(Guid sourceControlId, string branchName);
    }
}
