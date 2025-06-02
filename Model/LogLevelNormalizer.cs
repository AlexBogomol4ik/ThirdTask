using System;
using System.Collections.Generic;


namespace ThirdTask.Model
{
    public static class LogLevelNormalizer
    {
        private static readonly Dictionary<string, string> _mapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["INFORMATION"] = "INFO",
            ["INFO"] = "INFO",
            ["WARNING"] = "WARN",
            ["WARN"] = "WARN",
            ["ERROR"] = "ERROR",
            ["DEBUG"] = "DEBUG"
        };

        public static string Normalize(string logLevel)
        {
            return _mapping.TryGetValue(logLevel, out var normalized)
                ? normalized
                : throw new ArgumentException($"Неподдерживаемый уровень ведения журнала: {logLevel}");
        }
    }
}
