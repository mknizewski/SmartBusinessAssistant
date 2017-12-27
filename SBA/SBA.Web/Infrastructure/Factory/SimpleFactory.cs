using SimpleInjector.Integration.Web.Mvc;

namespace SBA.Web.Infrastructure.Factory
{
    public static class SimpleFactory
    {
        public static SimpleInjectorDependencyResolver Resolver =>
            new SimpleInjectorDependencyResolver(MvcApplication.Container);

        public static T CreateInstance<T>() where T : new () =>
            new T();
    }
}