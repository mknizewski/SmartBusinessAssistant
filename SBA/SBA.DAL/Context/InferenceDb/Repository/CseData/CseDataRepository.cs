using System.Linq;

namespace SBA.DAL.Context.InferenceDb.Repository.CseData
{
    public interface ICseDataRepository : IBaseRepository
    {
        IQueryable<Entity.CseData> GetCseDatas();
        IQueryable<Entity.CseData> GetUnhanldedCseDatas();
        Entity.CseData GetCseDataBy(int id);
    }

    public class CseDataRepository : BaseRepository, ICseDataRepository
    {
        public Entity.CseData GetCseDataBy(int id) =>
            Queryable<Entity.CseData>()
                .FirstOrDefault(x => x.Id == id);

        public IQueryable<Entity.CseData> GetCseDatas() =>
            Queryable<Entity.CseData>();

        public IQueryable<Entity.CseData> GetUnhanldedCseDatas() =>
            Queryable<Entity.CseData>()
                .Where(x => !x.IsHandled);
    }
}