using ThirdTask.Model;

namespace ThirdTask
{
    
    internal interface ILogParser
    {
        bool TryParse(string logLine, out LogEntry entry);
    }
}
