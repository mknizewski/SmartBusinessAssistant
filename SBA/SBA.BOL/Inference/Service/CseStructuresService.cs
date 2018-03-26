using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Json;
using SBA.BOL.Common.Factory;
using SBA.DAL.Context.InferenceDb.Entity;
using SBA.DAL.Context.InferenceDb.Repository.CseStructures;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ICseStructuresExtractService _cseStructuresExtractService;
        private static readonly object _lockObject = SimpleFactory.Get<object>();
        private static readonly string[] _supportedCseTypes =
        {
            ArticleCse.PageMapName,
            EventCse.PageMapName,
            OrganizationCse.PageMapName,
            PersonCse.PageMapName,
            SoftwareApplicationCse.PageMapName,
            SoftwareSourceCodeCse.PageMapName,
            VideoCse.PageMapName
        };

        public CseStructuresService()
        {
            _cseStructuresRepository = SimpleFactory.Get<CseStructuresRepository, ICseStructuresRepository>();
            _cseDataService = SimpleFactory.Get<CseDataService, ICseDataService>();
            _cseStructuresExtractService = SimpleFactory.Get<CseStructuresExtractService, ICseStructuresExtractService>();
        }

        public void StructurizeUnhandledCseData()
        {
            var unhanledCseDatas = _cseDataService.GetUnhandledCseDatas();
            Parallel.ForEach(unhanledCseDatas, cseData =>
            {
                var items = NewtonsoftJsonSerializer.Instance.Deserialize<IList<Result>>(cseData.RawJsonQueryResult);
                foreach (var item in items)
                {
                    var supportedTypes = item.Pagemap
                        .Where(type => _supportedCseTypes.Contains(type.Key))
                        .ToList();
                    var structuresList = GetStructureFromSupportedTypes(supportedTypes);

                    lock (_lockObject)
                    {
                        structuresList.ForEach(thing => {
                            thing.CseDataId = cseData.Id;
                            thing.Title = item.Title;
                            thing.Link = item.Link;
                            thing.DisplayLink = item.DisplayLink;
                            thing.Snippet = item.Snippet;
                            thing.InsertTime = DateTime.Now;

                            _cseStructuresRepository.AddThingWithRecognizeType(thing);
                        });

                        _cseDataService.SetCseDataHandled(cseData.Id);
                        _cseStructuresRepository.SaveChanges();
                    }
                }
            });
        }

        private List<ThingCse> GetStructureFromSupportedTypes(
            List<KeyValuePair<string, IList<IDictionary<string, object>>>> supportedTypes)
        {
            var structuresList = SimpleFactory.Get<List<ThingCse>>();
            foreach (var type in supportedTypes)
            {
                switch (type.Key)
                {
                    case ArticleCse.PageMapName:
                        structuresList.Add(_cseStructuresExtractService.GetArticleCseStruct(type));
                        break;
                    case EventCse.PageMapName:
                        structuresList.Add(_cseStructuresExtractService.GetEventCseStruct(type));
                        break;
                    case OrganizationCse.PageMapName:
                        structuresList.Add(_cseStructuresExtractService.GetOrganizationCseStruct(type));
                        break;
                    case PersonCse.PageMapName:
                        structuresList.Add(_cseStructuresExtractService.GetPersonCseStruct(type));
                        break;
                    case SoftwareApplicationCse.PageMapName:
                        structuresList.Add(_cseStructuresExtractService.GetSoftwareApplicationCseStruct(type));
                        break;
                    case SoftwareSourceCodeCse.PageMapName:
                        structuresList.Add(_cseStructuresExtractService.GetSoftwareSourceCodeCseStruct(type));
                        break;
                    case VideoCse.PageMapName:
                        structuresList.Add(_cseStructuresExtractService.GetVideoCseStruct(type));
                        break;
                    default:
                        break;
                }
            }

            return structuresList;
        }
    }
}