using SBA.Core.BOL.ThreadsSupervisior;

namespace SBA.Core.BOL.Infrastructure
{
    public static class Startup
    {
        public static void Run(string[] args)
        {
            Settings.RunTask(() => InputParamsHandler.HandleParams(args), "Handling params");
            Settings.RunTask(() => ThreadSupervisior.InitSupervisior(), "Initializing supervisior");
            Settings.RunTask(() => Settings.InitDatabase(), "Initializing database");
            Settings.RunTaskBackground(() => Settings.SeedWordVariety(), "Seed word variety");
            Settings.Supervisior
                .RegisterThreads()
                .Supervise();
        }
    }
}