using System.Collections.Generic;

namespace SBA.DAL.Context.WebDb.Repository.CookieFilter
{
    public interface ICookieFilterRepository : IBaseRepository
    {
        void SaveToLog(List<string> parametresToSave);
    }

    public class CookieFilterRepository : BaseRepository, ICookieFilterRepository
    {
        public void SaveToLog(List<string> parametresToSave)
        {
            //
        }
    }
}