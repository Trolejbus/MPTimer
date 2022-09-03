using System;

namespace WorkTimerWPF.Logic
{
    internal class LogInfo
    {
        public string Type { get; set; }
        public DateTime Date { get; set; }

        public LogInfo(string type, DateTime date)
        {
            Type = type;
            Date = date;
        }
    }
}
