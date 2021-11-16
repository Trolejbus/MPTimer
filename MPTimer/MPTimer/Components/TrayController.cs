using Autofac;
using MPTimerEventBus;

namespace MPTimer.Components
{
    internal class TrayController : IStartable
    {
        private readonly EventBus _bus;
        private NotifyIcon notifyIcon;
        private ToolStripItem? showMenuItem;

        public TrayController(EventBus bus)
        {
            _bus = bus;
            notifyIcon = new NotifyIcon()
            {
                Icon = Properties.Resources.clock,
                Visible = true
            };
        }

        public void Start()
        {
            _bus.Application.OnLoad += Init;
        }

        private void Init()
        {
            var contextMenu = CreateContextMenu();
            notifyIcon.ContextMenuStrip = contextMenu;
        }

        private ContextMenuStrip CreateContextMenu()
        {
            var menu = new ContextMenuStrip();
            showMenuItem = menu.Items.Add("Work Bar", null, (s,e) => Toggle());
            showMenuItem = menu.Items.Add("Open Dashboard", null, (s,e) => OpenDashboard());
            menu.Items.Add("-");
            menu.Items.Add("-");
            menu.Items.Add("Close", null, (s, e) => { Application.Exit(); });
            return menu;
        }

        private void OpenDashboard()
        {
            _bus.Application.InvokeOpenDashboard();
        }

        private void Toggle()
        {

        }
    }
}
