using SBA.Web.App_Start;
using SBA.Web.Infrastructure.BackgroundJobs;
using SBA.Web.Infrastructure.ContextInitializer;
using SBA.Web.Infrastructure.Factory;
using SBA.Web.Infrastructure.IoC;
using SBA.Web.Infrastructure.WebApi;
using SimpleInjector;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SBA.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static Container Container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.ConfigureMediaTypes();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Depedencies.Register();
            GlobalConfiguration.Configuration.SetApiResolver();
            DependencyResolver.SetResolver(SimpleFactory.Resolver);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            ContextInitializer.SbaWebInitializer.Init();
            BackgroundJobsConfigurator.Configure();
            BackgroundJobsConfigurator.RegisterBackgroundJobs();
        }
    }
}