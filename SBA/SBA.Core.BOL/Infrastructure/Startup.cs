using SBA.Core.BOL.ThreadsSupervisior;

namespace SBA.Core.BOL.Infrastructure
{
    public static class Startup
    {
        public static void Run(string[] args)
        {
            InputParamsHandler.HandleParams(args);
            ThreadSupervisior.InitSupervisior();
            Settings.Supervisior
                .RegisterThreads()
                .Supervise();
        }
    }
}