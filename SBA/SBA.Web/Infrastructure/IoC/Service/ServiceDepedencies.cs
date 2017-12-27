using SBA.BOL.Web.Service;
using SimpleInjector;

namespace SBA.Web.Infrastructure.IoC.Service
{
    public static class ServiceDepedencies
    {
        public static void Register()
        {
            MvcApplication.Container.Register<IConfigurationService, ConfigurationService>(Lifestyle.Scoped);
        }
    }
}