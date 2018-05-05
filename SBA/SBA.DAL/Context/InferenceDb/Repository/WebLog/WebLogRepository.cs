using System.Collections.Generic;
using System.Linq;

namespace SBA.DAL.Context.InferenceDb.Repository.WebLog
{
    public interface IWebLogRepository : IBaseRepository
    {
        void SaveWebLog(Entity.WebLog webLog);
        List<string> GetAllLinksDistinct();
        List<Entity.WebLog> GetWebLogs();
        void SetEntityProcessed(int id);
    }

    public class WebLogRepository : BaseRepository, IWebLogRepository
    {
        public List<string> GetAllLinksDistinct() =>
            Queryable<Entity.WebLog>()
                .Select(x => x.CurrentUrl)
                .Distinct()
                .ToList();

        public List<Entity.WebLog> GetWebLogs() =>
            Queryable<Entity.WebLog>()
                .Where(x => !x.IsProcessed)
                .ToList();

        public void SaveWebLog(Entity.WebLog webLog)
        {
            Add(webLog);
            SaveChanges();
        }

        public void SetEntityProcessed(int id)
        {
            var entity = Queryable<Entity.WebLog>()
                .FirstOrDefault(x => x.Id == id);

            entity.IsProcessed = true;
            SaveChanges();
        }
    }
}