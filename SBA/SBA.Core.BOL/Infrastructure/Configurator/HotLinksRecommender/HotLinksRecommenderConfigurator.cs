using SBA.Core.BOL.Threads;
using SBA.Core.BOL.Threads.HotLinksRecommender;

namespace SBA.Core.BOL.Infrastructure.Configurator.HotLinksRecommender
{
    public class HotLinksRecommenderConfigurator : IConfigurator<HotLinksRecommenderThread>
    {
        public HotLinksRecommenderThread Configure() => new HotLinksRecommenderThread
        {
            ExcecutionPlan = new ExcecutionPlan
            {
                ThreadName = nameof(HotLinksRecommenderThread),
                WorkAsSingleton = true,
                RunManually = true,
                ForceRun = false
            }
        };
    }
}