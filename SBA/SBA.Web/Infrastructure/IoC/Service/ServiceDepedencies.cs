using SBA.BOL.Web.Service;
using SimpleInjector;

namespace SBA.Web.Infrastructure.IoC.Service
{
    public static class ServiceDepedencies
    {
        public static void Register()
        {
            MvcApplication.Container.Register<IConfigurationService, ConfigurationService>(Lifestyle.Scoped);
            MvcApplication.Container.Register<IArticleService, ArticleService>(Lifestyle.Scoped);
            MvcApplication.Container.Register<IContactService, ContactService>(Lifestyle.Scoped);
            MvcApplication.Container.Register<IFileService, FileService>(Lifestyle.Scoped);
            MvcApplication.Container.Register<IClientSocketService, ClientSocketService>(Lifestyle.Scoped);
            MvcApplication.Container.Register<ICookieFilterService, CookieFilterService>(Lifestyle.Scoped);
        }
    }
}