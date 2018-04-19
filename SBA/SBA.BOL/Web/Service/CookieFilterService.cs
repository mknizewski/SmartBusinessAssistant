using SBA.DAL.Context.WebDb.Repository.CookieFilter;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SBA.BOL.Web.Service
{
    public interface ICookieFilterService
    {
        void SaveToLog(List<string> parametresToSave);
    }

    public class CookieFilterService : ICookieFilterService
    {
        private readonly ICookieFilterRepository _cookieFilterRepository;
        public CookieFilterService(ICookieFilterRepository cookieFilterRepository) =>
            _cookieFilterRepository = cookieFilterRepository;

        public void SaveToLog(List<string> parametresToSave)
        {
            string fileName = "web.log";
            string filePath = @"D:\git_folder\logs\web\" + parametresToSave[0];
            Directory.CreateDirectory(filePath);

            var file = new StringBuilder();
            var line = string.Format("{0} {1} {2} {3}\n",
                parametresToSave[1], parametresToSave[2], parametresToSave[3], parametresToSave[4]);
            file.Append(line);
            File.AppendAllText(filePath + @"\" + fileName, file.ToString());
        }
    }
}