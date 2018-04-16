using SBA.BOL.Common.Factory;
using SBA.DAL.Context.InferenceDb.Repository.Keywords;
using System.IO;
using System.Linq;

namespace SBA.BOL.Inference.Service
{
    public interface IKeywordService
    {
        MemoryStream GetKeywordsByStream();
    }

    public class KeywordService : IKeywordService
    {
        private readonly IKeywordRepository _keywordRepository;

        public KeywordService() =>
            _keywordRepository = SimpleFactory.Get<KeywordRepository, IKeywordRepository>();

        public MemoryStream GetKeywordsByStream()
        {
            var stream = SimpleFactory.Get<MemoryStream>();
            var keywords = _keywordRepository
                .GetKeywords()
                .ToList();

            using (var streamWriter = SimpleFactory.Get<StreamWriter>(stream))
                keywords.ForEach(keyword => streamWriter.WriteLine($"1,{keyword.Id},{keyword.Mark}"));

            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }
}