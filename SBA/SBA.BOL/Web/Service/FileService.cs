using System.IO;

namespace SBA.BOL.Web.Service
{
    public interface IFileService
    {
        string GetFileContent(string path);
    }

    public class FileService : IFileService
    {
        public string GetFileContent(string path)
        {
            using (var streamReader = new StreamReader(File.Open(path, FileMode.Open)))
                return streamReader.ReadToEnd();
        }
    }
}
