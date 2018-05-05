using SBA.Core.BOL.Infrastructure;
using System;
using System.IO;

namespace SBA.Core.BOL.Managers
{
    public interface ILoggerManager
    {
        void RegisterLogToFile(string log);
        void RegisterLogToConsole(string log, bool appendNewLine = true);
    }

    public class LoggerManager : ILoggerManager
    {
        private static readonly object _lockObject = SimpleFactory.Get<object>();

        public void RegisterLogToConsole(string log, bool appendNewLine = true)
        {
            if (appendNewLine)
                Console.WriteLine(log);
            else
                Console.Write(log);
        }

        public void RegisterLogToFile(string log)
        {
            lock (_lockObject)
            {
                var todayDate = DateTime.Now.ToString("yyy.MM.dd");
                string pathToLog = $"{Directory.GetCurrentDirectory()}\\{string.Format(Settings.Core.LogPathPattern, todayDate)}";
                string dateDirectory = Path.GetDirectoryName(pathToLog);

                if (!Directory.Exists(dateDirectory))
                    Directory.CreateDirectory(dateDirectory);

                File.AppendAllText(pathToLog, log);
            }
        }
    }
}
