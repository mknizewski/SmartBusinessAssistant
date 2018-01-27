using SBA.DAL.Context.WebDb.Entity;
using System.Collections.Generic;
using System.Linq;

namespace SBA.DAL.Context.WebDb.Repository.Files
{
    public interface IFileRepository : IBaseRepository
    {
        IEnumerable<File> GetFiles();
        File GetFileBy(int id);
    }

    public class FileRepository : BaseRepository, IFileRepository
    {
        public File GetFileBy(int id) =>
            Queryable<File>()
                .FirstOrDefault(x => x.Id == id);

        public IEnumerable<File> GetFiles() =>
            Queryable<File>()
                .ToList();
    }
}
