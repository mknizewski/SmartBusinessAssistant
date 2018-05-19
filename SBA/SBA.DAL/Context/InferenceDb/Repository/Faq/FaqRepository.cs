using SBA.DAL.Context.InferenceDb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SBA.DAL.Context.InferenceDb.Repository.Faq
{
    public interface IFaqRepository : IBaseRepository
    {
        List<FaqQuestions> GetFaqQuestions();
        List<FaqAnswers> GetFaqAnswers();
        void AddFaqDecision(FaqDecissions faqDecission);
        void AddFaqDecisions(List<FaqDecissions> faqDecissions);
        FaqAnswers GetAnswer(int id);
        void AddFaqQuestion(FaqQuestions faqQuestion);
        void DeleteFaqAnswer(int id);
        void DeleteFaqQuestion(int id);
        void EditFaqAnswer(int id, string answer);
        void EditFaqQuestion(int id, int answerId, string question);
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

        public void DeleteFaqAnswer(int id)
        {
            var faqAnswer = Queryable<FaqAnswers>()
                .FirstOrDefault(x => x.Id == id);

            Delete(faqAnswer);
            SaveChanges();
        }

        public void DeleteFaqQuestion(int id)
        {
            var faqQuestion = Queryable<FaqQuestions>()
                .FirstOrDefault(x => x.Id == id);

            Delete(faqQuestion);
            SaveChanges();
        }

        public void EditFaqAnswer(int id, string answer)
        {
            var faqAnswer = Queryable<FaqAnswers>()
                .FirstOrDefault(x => x.Id == id);

            faqAnswer.Answer = answer;
            SaveChanges();
        }

        public void EditFaqQuestion(int id, int answerId, string question)
        {
            var faqQuestion = Queryable<FaqQuestions>()
                .FirstOrDefault(x => x.Id == id);

            faqQuestion.AnswerId = answerId;
            faqQuestion.Question = question;

            SaveChanges();
        }

        public FaqAnswers GetAnswer(int id) =>
            Queryable<FaqAnswers>()
                .FirstOrDefault(x => x.Id == id);

        public List<FaqAnswers> GetFaqAnswers() =>
            Queryable<FaqAnswers>()
                .ToList();

        public List<FaqQuestions> GetFaqQuestions() =>
            Queryable<FaqQuestions>()
                .ToList();
    }
}