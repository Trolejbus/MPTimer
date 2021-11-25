namespace MPTimer.Models
{
    internal class TraySourceControl
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public TraySourceControl(Guid id, string name, string path)
        {
            Id = id;
            Name = name;
            Path = path;
        }
    }
}
