using SBA.DAL.Context.InferenceDb.Entity;
using System.Collections.Generic;
using System.Linq;

namespace SBA.DAL.Context.InferenceDb.Repository.Faq
{
    public interface IFaqRepository : IBaseRepository
    {
        List<FaqQuestions> GetFaqQuestions();
        FaqAnswers GetFaqAnswerByQuestionId(int questionId);
        void AddFaqDecision(FaqDecissions faqDecission);
    }

    public class FaqRepository : BaseRepository, IFaqRepository
    {
        public void AddFaqDecision(FaqDecissions faqDecission)
        {
            Add(faqDecission);
            SaveChanges();
        }

        public FaqAnswers GetFaqAnswerByQuestionId(int questionId) =>
            Queryable<FaqAnswers>()
                .FirstOrDefault(x => x.FaqQuestionId == questionId);

        public List<FaqQuestions> GetFaqQuestions() =>
            Queryable<FaqQuestions>()
                .ToList();
    }
}