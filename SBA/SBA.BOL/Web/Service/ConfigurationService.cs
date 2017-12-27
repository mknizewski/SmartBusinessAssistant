using SBA.BOL.Web.Models;
using SBA.DAL.Context.WebDb.Repository.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace SBA.BOL.Web.Service
{
    public interface IConfigurationService
    {
        List<ConfigurationModel> GetConfigurations();
    }

    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfigurationRepository _configurationRepository;

        public ConfigurationService(IConfigurationRepository configurationRepository) =>
            _configurationRepository = configurationRepository;

        public List<ConfigurationModel> GetConfigurations() =>
            _configurationRepository.GetConfigurations()
                .Select(x => new ConfigurationModel
                {
                    Key = x.Key,
                    Value = x.Name,
                    InsertDate = x.InsertTime.ToString("yyyy-MM-dd")
                }).ToList();
    }
}
