using SBA.Core.BOL.Threads;
using SBA.Core.BOL.Threads.StructureExtractor;

namespace SBA.Core.BOL.Infrastructure.Configurator.StructureExtractor
{
    public class StructureExtractorConfigurator : IConfigurator<StructureExtractorThread>
    {
        public StructureExtractorThread Configure() =>
            new StructureExtractorThread
            {
                ExcecutionPlan = new ExcecutionPlan
                {
                    ThreadName = nameof(StructureExtractorThread),
                    ExecuteTime = ExcecutionPlan.PreThreadTime.Hour,
                    WorkAsSingleton = true,
                    RunManually = false,
                    ForceRun = false
                }
            };
    }
}