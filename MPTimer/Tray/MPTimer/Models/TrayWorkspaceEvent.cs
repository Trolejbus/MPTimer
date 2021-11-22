using MPTimer.Enums;

namespace MPTimer.Models
{
    public class TrayWorkspaceEvent
    {
        public Guid Id { get; set; }
        public TrayWorkspaceEventType Type { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
        public Guid AgentId { get; set; }

        public TrayWorkspaceEvent(Guid id, TrayWorkspaceEventType type, DateTime from, Guid agentId)
        {
            Id = id;
            Type = type;
            From = from;
            AgentId = agentId;
        }
    }
}
