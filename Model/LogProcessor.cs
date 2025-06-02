using System;
using System.IO;

namespace ThirdTask.Model
{
    public class LogProcessor
    {
        public void Process(string inputPath, string outputPath, string problemsPath)
        {
            var reader = new StreamReader(inputPath);

 
            string line;
            while ((line = reader.ReadLine()) != null)
            {

                try
                {
                    var entry = LogParserFactory.ParseLine(line);
                    WriteEntry(outputPath, entry);
                }
                catch
                {
                    WriteProblem(problemsPath, line);
                }
            }
        }

        private async void WriteProblem(string path, string line)
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {

                await writer.WriteLineAsync(line);
            }

        }

        private async void WriteEntry(string path, LogEntry entry)
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                var date = entry.Timestamp.ToString("dd-MM-yyyy");
                var time = entry.Timestamp.ToString("HH:mm:ss.ffff").TrimEnd('0');

                await writer.WriteLineAsync($"{date}\t{time}\t{entry.LogLevel}\t{entry.CallerMethod}\t{entry.Message}");
            }

        }
    }
}
