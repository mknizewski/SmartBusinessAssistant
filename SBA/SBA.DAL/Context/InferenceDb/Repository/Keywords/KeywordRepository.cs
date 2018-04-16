using SBA.DAL.Context.InferenceDb.Entity;
using System.Linq;

namespace SBA.DAL.Context.InferenceDb.Repository.Keywords
{
    public interface IKeywordRepository : IBaseRepository
    {
        IQueryable<Keyword> GetKeywords();
        Keyword GetKeywordById(int id);
    }

    public class KeywordRepository : BaseRepository, IKeywordRepository
    {
        public Keyword GetKeywordById(int id) =>
            Queryable<Keyword>()
                .FirstOrDefault(x => x.Id == id);

        public IQueryable<Keyword> GetKeywords() =>
            Queryable<Keyword>();
    }
}