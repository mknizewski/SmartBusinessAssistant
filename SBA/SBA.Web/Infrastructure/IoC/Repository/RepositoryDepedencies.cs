using SBA.DAL.Context.WebDb.Repository;
using SBA.DAL.Context.WebDb.Repository.Configuration;
using SimpleInjector;

namespace SBA.Web.Infrastructure.IoC.Repository
{
    public static class RepositoryDepedencies
    {
        public static void Register()
        {
            MvcApplication.Container.Register<IBaseRepository, BaseRepository>(Lifestyle.Scoped);
            MvcApplication.Container.Register<IConfigurationRepository, ConfigurationRepository>(Lifestyle.Scoped);
        }
    }
}