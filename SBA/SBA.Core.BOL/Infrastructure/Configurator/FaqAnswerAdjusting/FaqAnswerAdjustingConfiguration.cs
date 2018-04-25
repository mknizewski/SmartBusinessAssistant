using SBA.Core.BOL.Threads;
using SBA.Core.BOL.Threads.FaqAnswerAdjusting;

namespace SBA.Core.BOL.Infrastructure.Configurator.FaqAnswerAdjusting
{
    public class FaqAnswerAdjustingConfiguration : IConfigurator<FaqAnswerAdjustingThread>
    {
        public FaqAnswerAdjustingThread Configure() => new FaqAnswerAdjustingThread
        {
            ExcecutionPlan = new ExcecutionPlan
            {
                ThreadName = nameof(FaqAnswerAdjustingThread),
                WorkAsSingleton = true,
                RunManually = true
            }
        };
    }
}