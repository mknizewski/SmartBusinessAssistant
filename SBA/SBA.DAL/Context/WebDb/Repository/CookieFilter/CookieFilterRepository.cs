using System.Collections.Generic;

namespace SBA.DAL.Context.WebDb.Repository.CookieFilter
{
    public interface ICookieFilterRepository : IBaseRepository
    {
        void SaveToLog(List<string> parametresToSave);
    }

    class CookieFilterRepository : BaseRepository, ICookieFilterRepository
    {
        public void SaveToLog(List<string> parametresToSave)
        {
            //
        }
    }
}
