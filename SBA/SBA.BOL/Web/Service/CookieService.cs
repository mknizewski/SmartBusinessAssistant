using SBA.BOL.Common.Factory;
using System;
using System.Configuration;
using System.IO;

namespace SBA.BOL.Web.Service
{
    public interface ICookieService
    {
        void SaveToLog(CookieService.CookieData cookieData);
    }

    public class CookieService : ICookieService
    {
        private static object _lockObject = SimpleFactory.Get<object>();

        public void SaveToLog(CookieData cookieData)
        {
            lock (_lockObject)
            {
                string webLogPath = string.Format(ConfigurationManager.AppSettings[nameof(webLogPath)], DateTime.Now.ToString("yyyy.MM.dd"));
                string webLogDirectory = Path.GetDirectoryName(webLogPath);

                if (!Directory.Exists(webLogDirectory))
                    Directory.CreateDirectory(webLogDirectory);

                using (var streamWriter = SimpleFactory.Get<StreamWriter>(SimpleFactory.Get<FileStream>(webLogPath, FileMode.Append, FileAccess.Write)))
                    streamWriter.WriteLine(cookieData);
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