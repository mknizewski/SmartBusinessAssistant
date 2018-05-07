using NReco.CF.Taste.Impl.Model.File;
using NReco.CF.Taste.Impl.Neighborhood;
using NReco.CF.Taste.Impl.Recommender;
using NReco.CF.Taste.Impl.Similarity;
using SBA.BOL.Inference.Models;
using SBA.BOL.Inference.Service;
using SBA.BOL.Web.Service;
using SBA.Core.BOL.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace SBA.Core.BOL.Threads.HotLinksRecommender
{
    public class HotLinksRecommenderThread : BaseThread, IThread
    {
        private readonly IWebLogService _webLogService;
        private readonly IWebLinkService _webLinkService;
        private readonly ICsvLogDataFileService _csvLogDataFileService;

        public HotLinksRecommenderThread()
        {
            _webLogService = SimpleFactory.Get<WebLogService, IWebLogService>();
            _webLinkService = SimpleFactory.Get<WebLinkService, IWebLinkService>();
            _csvLogDataFileService = SimpleFactory.Get<CsvLogDataFileService, ICsvLogDataFileService>();
        }

        public override T DoJob<T>()
        {
            string list = string.Empty;
            string userGuid = ExcecutionPlan.Parameters[0];
            string statTrace = ExcecutionPlan.Parameters[1];
            int userId = _webLogService.GetLastInsertedId();
            var webLinks = _webLinkService.GetWebLinks();
            var statTraceList = SimpleFactory.Get<List<string>>();
            var binaryFormatter = SimpleFactory.Get<BinaryFormatter>();
            var additionalCsvRows = SimpleFactory.Get<List<CsvDataFile.CsvDataFileRow>>();
            using (var memoryStream = SimpleFactory.Get<MemoryStream>(Convert.FromBase64String(statTrace)))
                statTraceList = (List<string>)binaryFormatter.Deserialize(memoryStream);

            foreach (var row in statTraceList)
            {
                string[] splited = row.Split(';');
                int linkId = _webLinkService.AddOrGetWebLink(new WebLinkModel { Url = splited[3] });
                var csvRow = additionalCsvRows
                    .FirstOrDefault(x => x.UrlId == linkId);

                if (csvRow != null)
                    csvRow.VisitCount += 1.0;
                else
                    additionalCsvRows.Add(new CsvDataFile.CsvDataFileRow
                    {
                        SessionId = userId,
                        UrlId = linkId,
                        VisitCount = 1.0
                    });
            }

            try
            {
                string csvFile = _csvLogDataFileService.GetTempCsvWithUserLogs(additionalCsvRows, Settings.CsvPath);
                var model = new FileDataModel(csvFile);
                var similarity = new EuclideanDistanceSimilarity(model);
                var neighborhood = new ThresholdUserNeighborhood(0.1, similarity, model);
                var recommender = new GenericBooleanPrefUserBasedRecommender(model, neighborhood, similarity);
                var recommendations = recommender.Recommend(userId, 3);

                foreach (var recommendation in recommendations)
                {
                    int itemId = (int) recommendation.GetItemID();
                    string link = webLinks
                        .Where(x => x.Id == itemId)
                        .Select(x => x.Url)
                        .FirstOrDefault();

                    list += $"<li><a href='{link}'>{link.Split('/').Last()}</a></li>";
                }

                _csvLogDataFileService.DeleteTempCsv(csvFile);
            }
            catch (Exception ex) { }

            return string.IsNullOrEmpty(list) ?
                   "Obecnie nie posiadamy dla Ciebie odpowiednich rekomendacji." as T :
                   $"<ul>{list}</ul>" as T;
        }
    }
}