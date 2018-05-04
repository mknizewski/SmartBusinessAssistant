using SBA.Core.BOL.Threads;
using SBA.Core.BOL.Threads.GarbageCollector;

namespace SBA.Core.BOL.Infrastructure.Configurator.GarbageCollector
{
    public class GarbageCollectorConfigurator : IConfigurator<GarbageCollectorThread>
    {
        public GarbageCollectorThread Configure() => new GarbageCollectorThread
        {
            ExcecutionPlan = new ExcecutionPlan
            {
                ThreadName = nameof(GarbageCollectorThread),
                ExecuteTime = ExcecutionPlan.PreThreadTime.Hour,
                WorkAsSingleton = true,
                RunManually = false
            }
        };
    }
}