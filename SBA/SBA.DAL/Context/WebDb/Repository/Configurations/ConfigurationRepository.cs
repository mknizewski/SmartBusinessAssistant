using System.Collections.Generic;
using System.Linq;

namespace SBA.DAL.Context.WebDb.Repository.Configuration
{
    public interface IConfigurationRepository : IBaseRepository
    {
        IEnumerable<Entity.Configuration> GetConfigurations();
    }

    public class ConfigurationRepository : BaseRepository, IConfigurationRepository
    {
        public IEnumerable<Entity.Configuration> GetConfigurations() =>
            Queryable<Entity.Configuration>()
                .ToList();
    }
}
