using SBA.Core.BOL.Threads;
using SBA.Core.BOL.Threads.GoogleCse;

namespace SBA.Core.BOL.Infrastructure.Configurator.GoogleCse
{
    public class GoogleCseConfigurator : IConfigurator<GoogleCseThread>
    {
        public GoogleCseThread Configure() => new GoogleCseThread
        {
            ExcecutionPlan = new ExcecutionPlan
            {
                ThreadName = nameof(GoogleCseThread),
                ExecuteTime = ExcecutionPlan.PreThreadTime.Hour,
                WorkAsSingleton = true,
                RunManually = true,
                ForceFirstRun = true
            }
        };
    }
}