using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ThirdTask.Model
{
    public class SecondFormatParser : ILogParser
    {
        private static readonly Regex _regex = new Regex(
            @"^(\d{4}-\d{2}-\d{2})\s(\d{2}:\d{2}:\d{2}\.\d+)\|([^|]+)\|\d+\|([^|]*)\|(.*)$",
            RegexOptions.Compiled);

        public bool TryParse(string logLine, out LogEntry entry)
        {
            entry = null;
            var match = _regex.Match(logLine);
            if (!match.Success) return false;

            try
            {
                var datePart = match.Groups[1].Value;
                var timePart = match.Groups[2].Value;
                var logLevel = match.Groups[3].Value.Trim();
                var callerMethod = match.Groups[4].Value.Trim();
                var message = match.Groups[5].Value.Trim();

                if (!DateTime.TryParseExact($"{datePart} {timePart}",
                    new[] { "yyyy-MM-dd HH:mm:ss.fff", "yyyy-MM-dd HH:mm:ss.ffff" },
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var timestamp))
                {
                    return false;
                }

                entry = new LogEntry
                {
                    Timestamp = timestamp,
                    LogLevel = LogLevelNormalizer.Normalize(logLevel),
                    CallerMethod = string.IsNullOrEmpty(callerMethod) ? "DEFAULT" : callerMethod,
                    Message = message
                };
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
