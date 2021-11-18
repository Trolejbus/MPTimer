using MPTimerAgent.Enums;

namespace MPTimerWeb.Dtos.Agent
{
    public class AgentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AgentType Type { get; set; }

        public AgentDto(Guid id, string name, AgentType type)
        {
            Id = id;
            Name = name;
            Type = type;
        }
    }
}
