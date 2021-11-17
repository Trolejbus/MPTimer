using MPTimerWorkTask.Models;

namespace MPTimerWorkTask.Interfaces
{
    public interface IWorkTaskRepository
    {
        Task<IEnumerable<WorkTask>> GetAll();
        Task<WorkTask> Add(WorkTask task);
    }
}
