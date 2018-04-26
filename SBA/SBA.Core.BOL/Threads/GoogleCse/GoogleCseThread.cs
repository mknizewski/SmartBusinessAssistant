using Google.Apis.Customsearch.v1;
using Google.Apis.Json;
using Google.Apis.Services;
using SBA.BOL.Inference.Models;
using SBA.BOL.Inference.Service;
using SBA.Core.BOL.Infrastructure;
using SBA.Core.BOL.Managers;
using System;

namespace SBA.Core.BOL.Threads.GoogleCse
{
    public class GoogleCseThread : BaseThread, IThread
    {
        private readonly ILoggerManager _loggerManager;
        private readonly ICseDataService _cseDataService;

        public GoogleCseThread()
        {
            _loggerManager = SimpleFactory.GetLogger();
            _cseDataService = SimpleFactory.Get<CseDataService, ICseDataService>();
        }

        public override T DoJob<T>()
        {
            string query = ExcecutionPlan.Parameters[0];
            var customSearchService = SimpleFactory
                .Get<CustomsearchService>(new BaseClientService.Initializer
                {
                    ApiKey = Settings.Core.CseEngineApiKey
                });
            
            string preQueryLog = $"{nameof(GoogleCseThread)} start finding items by query: {query}.";
            _loggerManager.RegisterLogToConsole(preQueryLog);
            _loggerManager.RegisterLogToFile(preQueryLog);

            var listRequest = customSearchService.Cse.List(query);
            listRequest.Cx = Settings.Core.CseEngineId;

            var response = listRequest.Execute();
            string json = NewtonsoftJsonSerializer.Instance.Serialize(response.Items);         
            _cseDataService.Add(new CseDataModel
            {
                Query = query,
                RawJsonQueryResult = json,
                ObjectType = typeof(Google.Apis.Customsearch.v1.Data.Result).FullName,
                InsertTime = DateTime.Now
            });

            return Nothing.All as T;
        }
    }
}