﻿using MPTimerWorkspaceEvent.Enums;

namespace MPTimerWorkspaceEvent.Models
{
    public class WorkspaceEvent
    {
        public Guid Id { get; set; }
        public WorkspaceEventType Type { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
        public string AgentId { get; set; }

        public WorkspaceEvent(Guid id, WorkspaceEventType type, DateTime from, string agentId)
        {
            Id = id;
            Type = type;
            From = from;
            AgentId = agentId;
        }
    }
}
