using Autofac;
using MPTimer.Interfaces;
using MPTimer.Models;

namespace MPTimer.Components
{
    internal class TrayController : IStartable
    {
        private readonly ISignalRController _signalRController;
        private readonly ISourceControlController _sourceControlController;
        private NotifyIcon notifyIcon;
        private ToolStripItem? signalRStatusItem;
        private IEnumerable<TraySourceControl> sourceControls = new List<TraySourceControl>();

        public TrayController(ISignalRController signalRController, ISourceControlController sourceControlController)
        {
            notifyIcon = new NotifyIcon()
            {
                Icon = Properties.Resources.clock,
                Visible = true
            };
            _signalRController = signalRController;
            _sourceControlController = sourceControlController;
        }

        public void Start()
        {
            var contextMenu = CreateContextMenu();
            notifyIcon.ContextMenuStrip = contextMenu;
            _signalRController.StatusChanged += _signalRController_StatusChanged;
            _sourceControlController.SourceControlLoaded += _sourceControlController_SourceControlLoaded;
            _sourceControlController.SourceControlStatusUpdated += _sourceControlController_SourceControlStatusUpdated;
        }

        private void _sourceControlController_SourceControlLoaded(IEnumerable<TraySourceControl> sourceControls)
        {
            var currentMenu = notifyIcon.ContextMenuStrip;
            if (currentMenu == null)
            {
                return;
            }

            this.sourceControls = sourceControls;
            foreach (var sourceControl in sourceControls)
            {
                currentMenu.Items.Insert(2, new ToolStripMenuItem(sourceControl.Name)
                {
                    Enabled = false,
                    Name = sourceControl.Id.ToString(),
                });
            }
        }

        private void _sourceControlController_SourceControlStatusUpdated(IEnumerable<TraySourceControlStatus> sourceControlStatuses)
        {
            var currentMenu = notifyIcon.ContextMenuStrip;
            if (currentMenu == null)
            {
                return;
            }

            if (notifyIcon.ContextMenuStrip.InvokeRequired)
            {
                notifyIcon.ContextMenuStrip.BeginInvoke(new MethodInvoker(() => UpdateSourceControlStatuses(sourceControlStatuses, currentMenu)));
            }
            else
            {
                UpdateSourceControlStatuses(sourceControlStatuses, currentMenu);
            }
        }

        private void UpdateSourceControlStatuses(IEnumerable<TraySourceControlStatus> sourceControlStatuses, ContextMenuStrip currentMenu)
        {
            foreach (var status in sourceControlStatuses)
            {
                var sourceControl = sourceControls.First(s => s.Id == status.SourceControlId);
                var item = currentMenu.Items.Find(status.SourceControlId.ToString(), false).FirstOrDefault();
                if (item == null)
                {
                    break;
                }

                item.Text = $"{sourceControl.Name} - {status.BranchName}";
            }
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
            menu.Items.Add("-");
            menu.Items.Add("Close", null, (s, e) => { Application.Exit(); });
            return menu;
        }
    }
}
