namespace SBA.Core.BOL.Threads
{
    public interface IThread
    {
        T DoJob<T>() where T : class;
        void RunJob();
    }
}