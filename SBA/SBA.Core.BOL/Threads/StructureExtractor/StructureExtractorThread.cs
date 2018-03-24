using SBA.BOL.Common.Factory;
using SBA.BOL.Inference.Service;

namespace SBA.Core.BOL.Threads.StructureExtractor
{
    public class StructureExtractorThread : BaseThread, IThread
    {
        private readonly ICseStructuresService _cseStructuresService;

        public StructureExtractorThread() => 
            _cseStructuresService = SimpleFactory.Get<CseStructuresService, ICseStructuresService>();

        public override void DoJob() =>
            _cseStructuresService.StructurizeUnhandledCseData();
    }
}