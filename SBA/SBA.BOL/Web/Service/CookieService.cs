using SBA.BOL.Common.Factory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace SBA.BOL.Web.Service
{
    public interface ICookieService
    {
        void SaveToLog(CookieService.CookieData cookieData);
        void SendLogsToCore();
    }

    public class CookieService : ICookieService
    {
        private static IClientSocketService _clientSocketService;
        private static object _lockObject = SimpleFactory.Get<object>();

        public CookieService() => 
            _clientSocketService = SimpleFactory.Get<ClientSocketService, IClientSocketService>();

        public void SaveToLog(CookieData cookieData)
        {
            lock (_lockObject)
            {
                string webLogPath = $"{AppDomain.CurrentDomain.BaseDirectory}{string.Format(ConfigurationManager.AppSettings[nameof(webLogPath)], DateTime.Now.ToString("yyyy.MM.dd"))}";
                string webLogDirectory = Path.GetDirectoryName(webLogPath);

                if (!Directory.Exists(webLogDirectory))
                    Directory.CreateDirectory(webLogDirectory);
                
                using (var streamWriter = SimpleFactory.Get<StreamWriter>(SimpleFactory.Get<FileStream>(webLogPath, FileMode.Append, FileAccess.Write)))
                    streamWriter.WriteLine(cookieData);
            }
        }

        public void SendLogsToCore()
        {
            string webLogPath = $"{AppDomain.CurrentDomain.BaseDirectory}{string.Format(ConfigurationManager.AppSettings[nameof(webLogPath)], DateTime.Now.ToString("yyyy.MM.dd"))}";
            string webCurrentLogDirectory = Path.GetDirectoryName(webLogPath);
            string webLogDirectory = Path.GetDirectoryName(webCurrentLogDirectory);
            var cookieData = SimpleFactory.Get<List<string>>();
            var directories = Directory.GetDirectories(webLogDirectory);

            foreach (var directory in directories)
            {
                string directoryName = Path.GetFileName(directory);
                if (!DateTime.TryParse(directoryName, out DateTime _) || 
                    directoryName == DateTime.Now.ToString("yyyy.MM.dd"))
                    continue;

                string logPath = $"{AppDomain.CurrentDomain.BaseDirectory}{string.Format(ConfigurationManager.AppSettings[nameof(webLogPath)], directoryName)}";
                using (var streamReader = SimpleFactory.Get<StreamReader>(SimpleFactory.Get<FileStream>(logPath, FileMode.Open, FileAccess.Read)))
                {
                    while (streamReader.Peek() >= 0)
                        cookieData.Add(streamReader.ReadLine());
                }

                if (cookieData.Count == 0)
                    continue;

                try
                {
                    _clientSocketService.SendLogsToCore(cookieData);
                    Directory.Move(directory, $"{directory} - Processed");
                }
                catch (Exception) { Debug.WriteLine("Core Layer Unaviable"); }
            }
        }

        public struct CookieData
        {
            public string SessionId { get; set; }
            public string CurrentTime { get; set; }
            public string ClientIp { get; set; }
            public string CurrentUrl { get; set; }
            public string PreviousUrl { get; set; }

            public override string ToString() => $"{SessionId};{CurrentTime};{ClientIp};{CurrentUrl};{PreviousUrl}";
        }
    }
}