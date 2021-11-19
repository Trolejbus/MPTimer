using MPTimerAgent.Enums;

namespace MPTimerAgent.Models
{
    public class AgentState
    {
        public Guid AgentId { get; set; }
        public AgentStatus Status { get; set; }
    }
}
