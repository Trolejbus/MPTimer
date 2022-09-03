using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using WorkTimerWPF.Logic;

namespace WorkTimerWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LogInfos.Entries.Add(new LogInfo("run", DateTime.Now));
            AppendFile($"run;{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            LoadLogInfox();
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            LogInfos.Entries.Add(new LogInfo("exit", DateTime.Now));
            AppendFile($"exit;{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            base.OnExit(e);
        }

        private static SessionSwitchEventHandler SystemEvents_SessionSwitch()
        {
            return (object sender, SessionSwitchEventArgs e) =>
            {
                if (e.Reason == SessionSwitchReason.SessionLock)
                {
                    LogInfos.Entries.Add(new LogInfo("lock", DateTime.Now));
                    AppendFile($"lock;{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                }

                else if (e.Reason == SessionSwitchReason.SessionUnlock)
                {
                    LogInfos.Entries.Add(new LogInfo("unlock", DateTime.Now));
                    AppendFile($"unlock;{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                }
            };
        }

        private static void AppendFile(string line)
        {
            string path = $"{DateTime.Now:yyyy-MM-dd}.txt";
            File.AppendAllLines(path, new[] { line });
        }

        private void LoadLogInfox()
        {
            string path = $"{DateTime.Now:yyyy-MM-dd}.txt";
            var lines = File.ReadAllLines(path);
            var actionInfos = lines.Select(l =>
            {
                var splitted = l.Split(';').ToArray();
                return new LogInfo(splitted[0], DateTime.Parse(splitted[1]));
            }).ToList();
            LogInfos.Entries = actionInfos;
        }
    }   
}
