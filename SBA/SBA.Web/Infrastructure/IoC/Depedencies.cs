using SBA.Web.Infrastructure.Factory;
using SBA.Web.Infrastructure.IoC.Repository;
using SBA.Web.Infrastructure.IoC.Service;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.WebApi;
using System.Reflection;
using System.Web.Http;

namespace SBA.Web.Infrastructure.IoC
{
    public static class Depedencies
    {
        public static void Register()
        {
            MvcApplication.Container = SimpleFactory.Get<Container>();
            MvcApplication.Container.Options.DefaultScopedLifestyle = SimpleFactory.Get<WebRequestLifestyle>();
            MvcApplication.Container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            MvcApplication.Container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            RepositoryDepedencies.Register();
            ServiceDepedencies.Register();

            MvcApplication.Container.Verify();
        }

        public static void SetApiResolver(this HttpConfiguration configuration) =>
            configuration.DependencyResolver = SimpleFactory.Get<SimpleInjectorWebApiDependencyResolver>(MvcApplication.Container);
    }
}