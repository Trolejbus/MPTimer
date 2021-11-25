namespace MPTimer.Models
{
    internal class TraySourceControlStatus
    {
        public Guid SourceControlId { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
        public string BranchName { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public TraySourceControlStatus()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

        public TraySourceControlStatus(Guid sourceControlId, DateTime from, string branchName)
        {
            SourceControlId = sourceControlId;
            From = from;
            BranchName = branchName;
        }
    }
}
