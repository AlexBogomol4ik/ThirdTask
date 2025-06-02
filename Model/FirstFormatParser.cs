using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ThirdTask.Model
{
    public class FirstFormatParser : ILogParser
    {
        private static readonly Regex _regex = new Regex(
            @"^(\d{2}\.\d{2}\.\d{4})\s(\d{2}:\d{2}:\d{2}\.\d+)\s+(\w+)\s+(.*)$",
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
                var logLevel = match.Groups[3].Value;
                var message = match.Groups[4].Value;

                if (!DateTime.TryParseExact($"{datePart} {timePart}",
                    new[] { "dd.MM.yyyy HH:mm:ss.fff", "dd.MM.yyyy HH:mm:ss.ffff" },
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var timestamp))
                {
                    return false;
                }

                entry = new LogEntry
                {
                    Timestamp = timestamp,
                    LogLevel = LogLevelNormalizer.Normalize(logLevel),
                    CallerMethod = "DEFAULT",
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
