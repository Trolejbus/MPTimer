using MPTimerAgent.Enums;

namespace MPTimerWeb.Dtos.Agent
{
    public class AgentStateDto
    {
        public Guid AgentId { get; set; }
        public AgentStatus Status { get; set; }
    }
}
