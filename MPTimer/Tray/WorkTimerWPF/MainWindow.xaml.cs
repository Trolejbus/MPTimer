using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using WorkTimerWPF.Logic;

namespace WorkTimerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler(OnTimerTick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnTimerTick(object? sender, EventArgs e)
        {
            var runningTime = TimeSpan.Zero;
            var lockTime = TimeSpan.Zero;
            DateTime? previousRun = null;
            DateTime? previousLock = null;
            foreach (var item in LogInfos.Entries)
            {
                if (item.Type == "run")
                {
                    previousRun = previousRun ?? item.Date;
                }

                if (item.Type == "lock")
                {
                    previousLock = previousLock ?? item.Date;
                }

                if (item.Type == "unlock")
                {
                    if (previousLock.HasValue)
                    {
                        lockTime += item.Date - previousLock.Value;
                        previousLock = null;
                    }
                }

                if (item.Type == "exit")
                {
                    if (previousRun.HasValue)
                    {
                        runningTime += item.Date - previousRun.Value;
                        previousRun = null;
                    }

                    if (previousLock.HasValue)
                    {
                        lockTime += item.Date - previousLock.Value;
                        previousLock = null;
                    }
                }
            }

            if (previousRun.HasValue)
            {
                runningTime += DateTime.Now - previousRun.Value;
            }

            if (previousLock.HasValue)
            {
                lockTime += DateTime.Now - previousLock.Value;
            }

            var workTime = runningTime - lockTime;

            RunningTimeMenuItem.Header = $"Running Time: {runningTime:hh\\:mm\\:ss}";
            LockTimeMenuItem.Header = $"Break Time: {lockTime:hh\\:mm\\:ss}";
            WorkTimeMenuItem.Header = $"Work Time: {workTime:hh\\:mm\\:ss}";
        }
    }
}
