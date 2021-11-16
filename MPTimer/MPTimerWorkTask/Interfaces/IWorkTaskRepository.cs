using MPTimerWorkTask.Models;

namespace MPTimerWorkTask.Interfaces
{
    public interface IWorkTaskRepository
    {
        Task<IEnumerable<WorkTask>> GetAll();
    }
}
