using MPTimerAgent.Enums;

namespace MPTimerWorkTask.Models
{
    public class Agent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AgentType Type { get; set; }

        public Agent(Guid id, string name, AgentType type)
        {
            Id = id;
            Name = name;
            Type = type;
        }
    }
}
