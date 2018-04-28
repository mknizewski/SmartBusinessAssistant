using SBA.DAL.Context.InferenceDb.Entity;
using System.Collections.Generic;
using System.Linq;

namespace SBA.DAL.Context.InferenceDb.Repository.Faq
{
    public interface IFaqRepository : IBaseRepository
    {
        List<FaqQuestions> GetFaqQuestions();
        void AddFaqDecision(FaqDecissions faqDecission);
        void AddFaqDecisions(List<FaqDecissions> faqDecissions);
        FaqAnswers GetAnswer(int id);
        void AddFaqQuestion(FaqQuestions faqQuestion);
    }

    public class FaqRepository : BaseRepository, IFaqRepository
    {
        public void AddFaqDecision(FaqDecissions faqDecission)
        {
            Add(faqDecission);
            SaveChanges();
        }

        public void AddFaqDecisions(List<FaqDecissions> faqDecissions)
        {
            AddRange(faqDecissions);
            SaveChanges();
        }

        public void AddFaqQuestion(FaqQuestions faqQuestion)
        {
            Add(faqQuestion);
            SaveChanges();
        }

        public FaqAnswers GetAnswer(int id) =>
            Queryable<FaqAnswers>()
                .FirstOrDefault(x => x.Id == id);

        public List<FaqQuestions> GetFaqQuestions() =>
            Queryable<FaqQuestions>()
                .ToList();
    }
}