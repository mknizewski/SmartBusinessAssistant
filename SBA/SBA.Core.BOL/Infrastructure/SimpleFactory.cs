using SBA.Core.BOL.Managers;
using System;

namespace SBA.Core.BOL.Infrastructure
{
    public static class SimpleFactory
    {
        public static T Get<T>() where T : new() =>
            new T();

        public static T Get<T>(params object[] constructorParams) =>
            (T)Activator.CreateInstance(typeof(T), constructorParams);

        public static I Get<T, I>() where T : I, new() =>
            new T();

        public static ILoggerManager GetLogger() =>
            new LoggerManager();
    }
}