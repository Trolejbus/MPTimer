using MPTimerUtils.Interfaces;

namespace MPTimerUtils.Service
{
    internal class RealTimeService : ITimeService
    {
        public DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }
    }
}
