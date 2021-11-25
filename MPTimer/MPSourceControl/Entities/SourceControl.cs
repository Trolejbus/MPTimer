namespace MPSourceControl.Entities
{
    public class SourceControl
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Guid AgentId { get; set; }
        public ICollection<SourceControlStatus>? Statuses { get; set; }

        public SourceControl(Guid id, string name, string path, Guid agentId)
        {
            Id = id;
            Name = name;
            Path = path;
            AgentId = agentId;
        }
    }
}
