using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SBA.BOL.Web.Service
{
    public interface ICookieFilterService
    {

    }

    public class CookieFilterService : ICookieFilterService
    {
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