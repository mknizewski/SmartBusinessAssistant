using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using SBA.Core.BOL.Infrastructure;

namespace SBA.Core.BOL.Threads.GoogleCse
{
    public class GoogleCseThread : BaseThread, IThread
    {
        public override void DoJob(params string[] jobParams)
        {
            string query = "programistok";
            var customSearchService = SimpleFactory
                .Get<CustomsearchService>(new BaseClientService.Initializer
                {
                    ApiKey = Settings.Core.CseEngineApiKey
                });


            var listRequest = customSearchService.Cse.List(query);
            listRequest.Cx = Settings.Core.CseEngineId;

            var response = listRequest.Execute();
            foreach (var item in response.Items)
            {
                System.Console.WriteLine(item.Title);
            }
        }
    }
}