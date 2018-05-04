namespace SBA.DAL.Context.InferenceDb.Repository.WebLog
{
    public interface IWebLogRepository : IBaseRepository
    {
        void SaveWebLog(Entity.WebLog webLog);
    }

    public class WebLogRepository : BaseRepository, IWebLogRepository
    {
        public void SaveWebLog(Entity.WebLog webLog)
        {
            Add(webLog);
            SaveChanges();
        }
    }
}