namespace MPTimer.Models
{
    internal class TraySourceControl
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string AgentId { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public TraySourceControl()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

        public TraySourceControl(Guid id, string agentId, string path, string name)
        {
            Id = id;
            Name = name;
            Path = path;
            AgentId = agentId;
        }
    }
}
