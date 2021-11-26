using MPSourceControl.Entities;

namespace MPSourceControl.Interfaces
{
    public interface ISourceControlRepository
    {
        Task<IEnumerable<SourceControl>> GetAll();
        Task<SourceControl> Add(SourceControl task);
        Task Remove(Guid id);
        Task<SourceControl> GetById(Guid id);
        Task<SourceControl> Update(Guid id, SourceControl model);
        Task<SourceControl> ChangeBranch(Guid id, string branch);
        Task TerminateAllSourceControls(Guid agentIdGuid);
    }
}
