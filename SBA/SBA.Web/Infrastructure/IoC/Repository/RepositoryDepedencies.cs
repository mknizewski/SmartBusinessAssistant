using SBA.DAL.Context.WebDb.Repository;
using SBA.DAL.Context.WebDb.Repository.Articles;
using SBA.DAL.Context.WebDb.Repository.Configuration;
using SBA.DAL.Context.WebDb.Repository.Contacts;
using SBA.DAL.Context.WebDb.Repository.Files;
using SimpleInjector;

namespace SBA.Web.Infrastructure.IoC.Repository
{
    public static class RepositoryDepedencies
    {
        public static void Register()
        {
            MvcApplication.Container.Register<IBaseRepository, BaseRepository>(Lifestyle.Scoped);
            MvcApplication.Container.Register<IConfigurationRepository, ConfigurationRepository>(Lifestyle.Scoped);
            MvcApplication.Container.Register<IArticleRepository, ArticleRepository>(Lifestyle.Scoped);
            MvcApplication.Container.Register<IContactRepository, ContactRepository>(Lifestyle.Scoped);
            MvcApplication.Container.Register<IFileRepository, FileRepository>(Lifestyle.Scoped);
        }
    }
}