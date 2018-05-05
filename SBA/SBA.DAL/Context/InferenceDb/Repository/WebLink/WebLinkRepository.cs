using SBA.DAL.Context.InferenceDb.Entity;
using System.Collections.Generic;
using System.Linq;

namespace SBA.DAL.Context.InferenceDb.Repository.WebLink
{
    public interface IWebLinkRepository : IBaseRepository
    {
        List<WebLinks> GetWebLinks();
        int AddOrGetWebLink(WebLinks webLink);
    }

    public class WebLinkRepository : BaseRepository, IWebLinkRepository
    {
        public int AddOrGetWebLink(WebLinks webLink)
        {
            var entity = Queryable<WebLinks>()
                .FirstOrDefault(x => x.Url == webLink.Url);

            if (entity != null)
                return entity.Id;

            Add(webLink);
            SaveChanges();

            return webLink.Id;
        }

        public List<WebLinks> GetWebLinks() =>
            Queryable<WebLinks>()
                .ToList();
    }
}