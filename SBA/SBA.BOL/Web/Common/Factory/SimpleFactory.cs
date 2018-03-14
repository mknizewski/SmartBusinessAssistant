using System;

namespace SBA.BOL.Web.Common.Factory
{
    public static class SimpleFactory
    {
        public static T Get<T>() where T : new() =>
           new T();

        public static T Get<T>(params object[] constructorParams) =>
            (T)Activator.CreateInstance(typeof(T), constructorParams);

        public static I Get<T, I>() where T : I, new() =>
            new T();
    }
}
