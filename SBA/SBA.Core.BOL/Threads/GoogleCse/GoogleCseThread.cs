using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using SBA.Core.BOL.Infrastructure;

namespace SBA.Core.BOL.Threads.GoogleCse
{
    public class GoogleCseThread : BaseThread, IThread
    {
        public override void DoJob(params string[] jobParams)
        {
            string query = "wydarzenia informatyczne białystok";
            var customSearchService = SimpleFactory
                .Get<CustomsearchService>(new BaseClientService.Initializer
                {
                    ApiKey = Settings.Core.CseEngineApiKey
                });

            var listRequest = customSearchService.Cse.List(query);
            listRequest.Cx = Settings.Core.CseEngineId;
            listRequest.Num = Settings.Core.CseCountResult;

            var response = listRequest.Execute();
        }
    }
}