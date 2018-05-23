namespace SBA.Client.Wpf.BOL.Infrastucture
{
    public static class SimpleFactory
    {
        public static I Get<T, I>() where T : I, new() => SBA.BOL.Common.Factory.SimpleFactory.Get<T, I>();
        public static T Get<T>() where T : new() => SBA.BOL.Common.Factory.SimpleFactory.Get<T>();
        public static T Get<T>(params object[] constructorParams) where T : new() => SBA.BOL.Common.Factory.SimpleFactory.Get<T>(constructorParams);
    }
}