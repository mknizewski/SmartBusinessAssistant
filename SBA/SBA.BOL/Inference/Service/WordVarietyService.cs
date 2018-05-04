using SBA.BOL.Common.Factory;
using SBA.DAL.Context.InferenceDb.Repository.WordVariety;

namespace SBA.BOL.Inference.Service
{
    public interface IWordVarietyService
    {
        string Lemmatize(string word);
    }

    public class WordVarietyService : IWordVarietyService
    {
        private readonly IWordVarietyRepository _wordVarietyRepository;

        public WordVarietyService() => 
            _wordVarietyRepository = SimpleFactory.Get<WordVarietyRepository, IWordVarietyRepository>();

        public string Lemmatize(string word) =>
            _wordVarietyRepository.Lemmatize(word);
    }
}