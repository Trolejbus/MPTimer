using MPTimerAgent.Enums;

namespace MPTimerAgent.Entities
{
    public class Agent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AgentType Type { get; set; }

        public ICollection<AgentRuntime> AgentRuntimes { get; set; } = new List<AgentRuntime>();

        public Agent(Guid id, string name, AgentType type)
        {
            Id = id;
            Name = name;
            Type = type;
        }
    }
}
