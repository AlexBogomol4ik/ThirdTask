using System;

namespace ThirdTask.Model
{
    public class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public string LogLevel { get; set; }
        public string CallerMethod { get; set; }
        public string Message { get; set; }
    }
}
