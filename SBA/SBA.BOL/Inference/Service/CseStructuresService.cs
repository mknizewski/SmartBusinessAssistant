using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Json;
using SBA.BOL.Common.Factory;
using SBA.DAL.Context.InferenceDb.Repository.CseStructures;
using System.Threading.Tasks;

namespace SBA.BOL.Inference.Service
{
    public interface ICseStructuresService
    {
        void StructurizeUnhandledCseData();
    }

    public class CseStructuresService : ICseStructuresService
    {
        private readonly ICseStructuresRepository _cseStructuresRepository;
        private readonly ICseDataService _cseDataService;
        private static readonly object _syncObject = SimpleFactory.Get<object>();

        public CseStructuresService()
        {
            _cseStructuresRepository = SimpleFactory.Get<CseStructuresRepository, ICseStructuresRepository>();
            _cseDataService = SimpleFactory.Get<CseDataService, ICseDataService>();
        }

        public void StructurizeUnhandledCseData()
        {
            var unhanledCseDatas = _cseDataService.GetUnhandledCseDatas();
            Parallel.ForEach(unhanledCseDatas, cseData =>
            {
                var result = NewtonsoftJsonSerializer.Instance.Deserialize<Result>(cseData.RawJsonQueryResult);
            });
        }
    }
}