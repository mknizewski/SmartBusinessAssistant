using SBA.Core.BOL.Threads;
using SBA.Core.BOL.Threads.KeywordRecommender;

namespace SBA.Core.BOL.Infrastructure.Configurator.KeywordRecommender
{
    public class KeywordRecommenderConfigurator : IConfigurator<KeywordRecommenderThread>
    {
        public KeywordRecommenderThread Configure() =>
            new KeywordRecommenderThread
            {
                ExcecutionPlan = new ExcecutionPlan
                {
                    ThreadName = nameof(KeywordRecommenderThread),
                    ExecuteTime = ExcecutionPlan.PreThreadTime.Hour,
                    WorkAsSingleton = true,
                    RunManually = false,
                    ForceRun = false
                }
            };
    }
}