using Autofac;
using MPTimerEventBus;
using MPTimerUtils.Interfaces;
using MPTimerUtils.Service;
using System.ComponentModel;

namespace MPTimerDashboard.Components
{
    public partial class TimerStatus : UserControl
    {
        private readonly ITimeService? timeService;

        public TimerStatus()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            var scope = ScopeSingleton.Scope;
            var eventBus = scope.Resolve<EventBus>();
            timeService = scope.Resolve<ITimeService>();
            eventBus.Timer.OnTick += Timer_OnTick;
        }

        private void Timer_OnTick()
        {
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            currentTimeLabel.Text = timeService?.GetCurrentDate().ToString("yyyy-MM-dd hh:mm:ss");
        }
    }
}
