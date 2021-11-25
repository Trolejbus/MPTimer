namespace MPSourceControl.Entities
{
    public class SourceControlStatus
    {
        public Guid Id { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
        public string BranchName { get; set; }
        public Guid SourceControlId { get; set; }

        public SourceControlStatus(Guid id, string branchName, DateTime from, Guid sourceControlId)
        {
            Id = id;
            BranchName = branchName;
            From = from;
            SourceControlId = sourceControlId;
        }
    }
}
