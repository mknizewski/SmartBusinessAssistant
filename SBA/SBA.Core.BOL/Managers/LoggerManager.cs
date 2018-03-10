using SBA.Core.BOL.Infrastructure;
using System;
using System.IO;

namespace SBA.Core.BOL.Managers
{
    public interface ILoggerManager
    {
        void RegisterLog(string log);
    }

    public class LoggerManager : ILoggerManager
    {
        private object _lockObject = new object();

        public void RegisterLog(string log)
        {
            lock (_lockObject)
            {
                var todayDate = DateTime.Now.ToString("yyy.MM.dd");
                string pathToLog = string.Format(Settings.Core.LogPathPattern, todayDate);
                string dateDirectory = Path.GetDirectoryName(pathToLog);

                if (!Directory.Exists(dateDirectory))
                    Directory.CreateDirectory(dateDirectory);

                File.AppendAllText(pathToLog, log);
            }
        }
    }
}
