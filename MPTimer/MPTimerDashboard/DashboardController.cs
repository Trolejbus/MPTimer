using Autofac;
using MPTimerEventBus;

namespace MPTimer.Components.Dashboard
{
    internal class DashboardController : IStartable
    {
        private readonly EventBus _eventBus;
        private DashboardForm? _dashboardForm;

        public DashboardController(EventBus mpTimerEventBus)
        {
            _eventBus = mpTimerEventBus;
        }

        public void Start()
        {
            _eventBus.Application.OnOpenDashboard += OnEvent_OpenDashboard;
        }

        private void OnEvent_OpenDashboard()
        {
            if (_dashboardForm == null)
            {
                _dashboardForm = new DashboardForm();
                _dashboardForm.FormClosed += (s, e) => _dashboardForm = null;
            }

            _dashboardForm.Show();
            _dashboardForm.BringToFront();
        }
    }
}
