using System.Linq;

namespace SBA.DAL.Context.InferenceDb.Repository.CseData
{
    public interface ICseDataRepository : IBaseRepository
    {
        IQueryable<Entity.CseData> GetCseDatas();
        Entity.CseData GetCseDataBy(int id);
    }

    public class CseDataRepository : BaseRepository, ICseDataRepository
    {
        public Entity.CseData GetCseDataBy(int id) =>
            Queryable<Entity.CseData>()
                .FirstOrDefault(x => x.Id == id);

        public IQueryable<Entity.CseData> GetCseDatas() =>
            Queryable<Entity.CseData>();
    }
}