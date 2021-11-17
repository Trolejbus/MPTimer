using MPTimerWorkTask.Models;

namespace MPTimerWorkTask.Interfaces
{
    public interface IWorkTaskRepository
    {
        Task<IEnumerable<WorkTask>> GetAll();
        Task<WorkTask> Add(WorkTask task);
        Task Remove(Guid id);
        Task<WorkTask> GetById(Guid id);
        Task<WorkTask> Update(Guid id, WorkTask model);
    }
}
