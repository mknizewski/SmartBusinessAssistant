using SBA.DAL.Context.InferenceDb.Entity;
using System.Linq;

namespace SBA.DAL.Context.InferenceDb.Repository.WebSessions
{
    public interface IWebSessionRepository : IBaseRepository
    {
        int AddOrGetWebSessionId(WebSessionsId webSessionId);
    }

    public class WebSessionRepository : BaseRepository, IWebSessionRepository
    {
        public int AddOrGetWebSessionId(WebSessionsId webSessionId)
        {
            var entity = Queryable<WebSessionsId>()
                .FirstOrDefault(x => x.SessionId == webSessionId.SessionId);

            if (entity != null)
                return entity.Id;

            Add(webSessionId);
            SaveChanges();

            return webSessionId.Id;
        }
    }
}