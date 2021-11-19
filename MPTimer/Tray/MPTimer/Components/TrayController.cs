using Autofac;
using MPTimer.Interfaces;

namespace MPTimer.Components
{
    internal class TrayController : IStartable
    {
        private readonly ISignalRController _signalRController;

        private NotifyIcon notifyIcon;
        private ToolStripItem? signalRStatusItem;

        public TrayController(ISignalRController signalRController)
        {
            notifyIcon = new NotifyIcon()
            {
                Icon = Properties.Resources.clock,
                Visible = true
            };
            _signalRController = signalRController;
        }

        public void Start()
        {
            var contextMenu = CreateContextMenu();
            notifyIcon.ContextMenuStrip = contextMenu;
            _signalRController.StatusChanged += _signalRController_StatusChanged;
        }

        private void _signalRController_StatusChanged(Enums.SignalRConnectionStatus obj)
        {
            if (signalRStatusItem == null)
            {
                throw new Exception("signalRStatusItem was not created yet");
            }

            signalRStatusItem.Text = _signalRController.Status.ToString();
        }

        private ContextMenuStrip CreateContextMenu()
        {
            var menu = new ContextMenuStrip();
            signalRStatusItem = new ToolStripMenuItem(_signalRController.Status.ToString());
            signalRStatusItem.Enabled = false;
            menu.Items.Add(signalRStatusItem);
            menu.Items.Add("-");
            menu.Items.Add("Close", null, (s, e) => { Application.Exit(); });
            return menu;
        }
    }
}
