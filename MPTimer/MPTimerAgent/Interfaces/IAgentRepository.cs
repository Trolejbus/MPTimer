using MPTimerWorkTask.Models;

namespace MPTimerWorkTask.Interfaces
{
    public interface IAgentRepository
    {
        Task<IEnumerable<Agent>> GetAll();
        Task<Agent> Add(Agent task);
        Task Remove(Guid id);
        Task<Agent> GetById(Guid id);
        Task<Agent> Update(Guid id, Agent model);
    }
}
