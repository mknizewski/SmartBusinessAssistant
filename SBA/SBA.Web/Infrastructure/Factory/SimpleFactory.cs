﻿using SimpleInjector.Integration.Web.Mvc;
using System;

namespace SBA.Web.Infrastructure.Factory
{
    public static class SimpleFactory
    {
        public static SimpleInjectorDependencyResolver Resolver =>
            new SimpleInjectorDependencyResolver(MvcApplication.Container);

        public static T Get<T>() where T : new() =>
           new T();

        public static T Get<T>(params object[] constructorParams) =>
            (T)Activator.CreateInstance(typeof(T), constructorParams);

        public static I Get<T, I>() where T : I, new() =>
            new T();
    }
}