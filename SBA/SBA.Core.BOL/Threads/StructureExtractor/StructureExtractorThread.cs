using SBA.BOL.Inference.Service;
using SBA.Core.BOL.Infrastructure;

namespace SBA.Core.BOL.Threads.StructureExtractor
{
    public class StructureExtractorThread : BaseThread, IThread
    {
        private readonly ICseStructuresService _cseStructuresService;

        public StructureExtractorThread() => 
            _cseStructuresService = SimpleFactory.Get<CseStructuresService, ICseStructuresService>();

        public override T DoJob<T>()
        {
            _cseStructuresService.StructurizeUnhandledCseData();
            return Nothing.All as T;
        }
    }
}