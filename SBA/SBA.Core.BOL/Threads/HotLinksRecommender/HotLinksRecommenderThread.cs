using NReco.CF.Taste.Impl.Model.File;
using NReco.CF.Taste.Impl.Neighborhood;
using NReco.CF.Taste.Impl.Recommender;
using NReco.CF.Taste.Impl.Similarity;
using NReco.CF.Taste.Model;
using SBA.BOL.Common.Factory;
using SBA.BOL.Inference.Service;
using SBA.BOL.Web.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SBA.Core.BOL.Threads.HotLinksRecommender
{
    public class HotLinksRecommenderThread : BaseThread, IThread
    {
        private readonly ICookieService _cookieService;
        private readonly IWebLogService _webLogService;
        private readonly IWebLinkService _webLinkService;

        public HotLinksRecommenderThread()
        {
            _cookieService = SimpleFactory.Get<CookieService, ICookieService>();
            _webLogService = SimpleFactory.Get<WebLogService, IWebLogService>();
            _webLinkService = SimpleFactory.Get<WebLinkService, IWebLinkService>();
        }

        public override T DoJob<T>()
        {
            string userGuid = ExcecutionPlan.Parameters[0];
            string statTrace = ExcecutionPlan.Parameters[1];
            var statTraceList = SimpleFactory.Get<List<string>>();
            var binaryFormatter = SimpleFactory.Get<BinaryFormatter>();
            using (var memoryStream = SimpleFactory.Get<MemoryStream>(Convert.FromBase64String(statTrace)))
                statTraceList = (List<string>)binaryFormatter.Deserialize(memoryStream);

            //var model = new FileDataModel("");
            //var similarity = new PearsonCorrelationSimilarity(model);
            //var neighborhood = new ThresholdUserNeighborhood(0.1, similarity, model);
            //var recommender = new GenericUserBasedRecommender(model, neighborhood, similarity);
            //var recommendations = recommender.Recommend()

            return "JEST OK" as T;
        }
    }
}