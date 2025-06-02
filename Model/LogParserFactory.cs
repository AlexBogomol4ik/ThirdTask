using System;
using System.Collections.Generic;


namespace ThirdTask.Model
{
    public static class LogParserFactory
    {
        private static readonly List<ILogParser> _parsers = new List<ILogParser>
        {
            new FirstFormatParser(),
            new SecondFormatParser()
        };

        public static LogEntry ParseLine(string logLine)
        {
            foreach (var parser in _parsers)
            {
                if (parser.TryParse(logLine, out var entry))
                {
                    return entry;
                }
            }
            throw new FormatException("Подходящий синтаксический анализатор не найден");
        }
    }
}
