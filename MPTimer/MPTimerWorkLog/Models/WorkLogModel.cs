using MPTimerWorkLog.Enums;

namespace MPTimerWorkLog.Models
{
    public class WorkLogModel
    {
        public Guid TaskId { get; set; }

        public WorkType Type { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
