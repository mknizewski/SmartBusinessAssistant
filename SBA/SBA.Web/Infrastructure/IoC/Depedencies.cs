using SBA.Web.Controllers;
using SBA.Web.Infrastructure.Factory;
using SBA.Web.Infrastructure.IoC.Repository;
using SBA.Web.Infrastructure.IoC.Service;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using System.Reflection;
using System.Web.Mvc;

namespace SBA.Web.Infrastructure.IoC
{
    public static class Depedencies
    {
        public static void Register()
        {
            MvcApplication.Container = SimpleFactory.CreateInstance<Container>();
            MvcApplication.Container.Options.DefaultScopedLifestyle =
                SimpleFactory.CreateInstance<WebRequestLifestyle>();
            MvcApplication.Container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            RepositoryDepedencies.Register();
            ServiceDepedencies.Register();

            MvcApplication.Container.Verify();
        }
    }
}