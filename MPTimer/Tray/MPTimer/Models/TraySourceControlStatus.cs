namespace MPTimer.Models
{
    internal class TraySourceControlStatus
    {
        public TraySourceControl SourceControl { get; set; }
        public string? Branch { get; set; }

        public TraySourceControlStatus(TraySourceControl sourceControl)
        {
            SourceControl = sourceControl;
        }
    }
}
