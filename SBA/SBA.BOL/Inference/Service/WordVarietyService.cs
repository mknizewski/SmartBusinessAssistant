using SBA.BOL.Common.Factory;
using SBA.DAL.Context.InferenceDb.Repository.WordVariety;
using System.Linq;

namespace SBA.BOL.Inference.Service
{
    public interface IWordVarietyService
    {
        string[] Lemmatize(string[] words);
    }

    public class WordVarietyService : IWordVarietyService
    {
        private readonly IWordVarietyRepository _wordVarietyRepository;

        public WordVarietyService() => 
            _wordVarietyRepository = SimpleFactory.Get<WordVarietyRepository, IWordVarietyRepository>();

        public string[] Lemmatize(string[] words) => 
            words
            .Select(word => _wordVarietyRepository.Lemmatize(word))
            .ToArray();
    }
}