using System.Linq;

namespace SBA.DAL.Context.InferenceDb.Repository.WordVariety
{
    public interface IWordVarietyRepository : IBaseRepository
    {
        string Lemmatize(string word);
    }

    public class WordVarietyRepository : BaseRepository, IWordVarietyRepository
    {
        public string Lemmatize(string word)
        {
            var dbVariety = Set<Entity.WordVariety>()
                .FirstOrDefault(x => x.Word == word);
            
            if (dbVariety == null)
                return word;

            if (dbVariety.OrginalWordId.HasValue)
                return dbVariety.OrginalWord.Word;

            return dbVariety.Word;
        }
    }
}