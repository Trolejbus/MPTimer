namespace MPTimerAgent.Entities
{
    public class AgentRuntime
    {
        public Guid Id { get; set; }
        public Guid AgentId { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }

        public AgentRuntime(Guid agentId, DateTime from)
        {
            AgentId = agentId;
            From = from;
        }
    }
}
