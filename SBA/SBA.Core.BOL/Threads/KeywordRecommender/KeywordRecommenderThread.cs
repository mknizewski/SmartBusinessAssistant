using SBA.BOL.Common.Factory;
using SBA.BOL.Inference.Service;
using System;

namespace SBA.Core.BOL.Threads.KeywordRecommender
{
    public class KeywordRecommenderThread : BaseThread, IThread
    {
        private readonly IKeywordService _keywordService;

        public KeywordRecommenderThread() =>
            _keywordService = SimpleFactory.Get<KeywordService, IKeywordService>();

        public override void DoJob()
        {
            
        }
    }
}