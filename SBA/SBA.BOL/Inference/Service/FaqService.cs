using SBA.BOL.Common.Factory;
using SBA.BOL.Inference.Models;
using SBA.DAL.Context.InferenceDb.Entity;
using SBA.DAL.Context.InferenceDb.Repository.Faq;
using System.Collections.Generic;
using System.Linq;

namespace SBA.BOL.Inference.Service
{
    public interface IFaqService
    {
        List<FaqModel.Question> GetFaqQuestions();
        List<FaqModel.Answer> GetFaqAnswers();
        void AddFaqDecision(FaqModel.Decide faqDecission);
        void AddFaqDecisions(List<FaqModel.Decide> decides);
        string GetAnswer(int answerId);
        void AddFaqQuestion(FaqModel.Question faqQuestion);
        void DeleteFaqAnswer(int id);
        void DeleteFaqQuestion(int id);
        void EditFaqAnswer(int id, string answer);
        void EditFaqQuestion(int id, int answerId, string question);
    }

    public class FaqService : IFaqService
    {
        private readonly IFaqRepository _faqRespository;

        public FaqService() =>
            _faqRespository = SimpleFactory.Get<FaqRepository, IFaqRepository>();

        public void AddFaqDecision(FaqModel.Decide faqDecission) =>
            _faqRespository.AddFaqDecision(new FaqDecissions
            {
                Propability = faqDecission.Propability,
                Decide = faqDecission.DecideStatus,
                FaqAnswerId = faqDecission.AnswerId,
                Classificator = faqDecission.Classificator
            });

        public void AddFaqDecisions(List<FaqModel.Decide> decides) =>
            _faqRespository.AddFaqDecisions(
                decides.Select(x => new FaqDecissions
                {
                    Classificator = x.Classificator,
                    Decide = x.DecideStatus,
                    FaqAnswerId = x.AnswerId,
                    Propability = x.Propability
                }).ToList());

        public void AddFaqQuestion(FaqModel.Question faqQuestion)
        {
            var entity = _faqRespository
                .Queryable<FaqQuestions>()
                .FirstOrDefault(x => x.Question.ToLower() == faqQuestion.QuestionName.ToLower());

            if (entity != null)
                return;

            _faqRespository.AddFaqQuestion(new FaqQuestions
            {
                AnswerId = faqQuestion.AnswerId,
                Question = faqQuestion.QuestionName,
                InsertTime = faqQuestion.InsertTime
            });
        }

        public void DeleteFaqAnswer(int id) =>
            _faqRespository.DeleteFaqAnswer(id);

        public void DeleteFaqQuestion(int id) =>
            _faqRespository.DeleteFaqQuestion(id);

        public void EditFaqAnswer(int id, string answer) =>
            _faqRespository.EditFaqAnswer(id, answer);

        public void EditFaqQuestion(int id, int answerId, string question) =>
            _faqRespository.EditFaqQuestion(id, answerId, question);

        public string GetAnswer(int answerId)
        {
            var answer = _faqRespository.GetAnswer(answerId);
            return answer.Answer;
        }

        public List<FaqModel.Answer> GetFaqAnswers() =>
            _faqRespository.GetFaqAnswers()
                .Select(x => new FaqModel.Answer
                {
                    Id = x.Id,
                    AnswerName = x.Answer
                }).ToList();

        public List<FaqModel.Question> GetFaqQuestions() =>
            _faqRespository.GetFaqQuestions()
                .Select(x => new FaqModel.Question
                {
                    Id = x.Id,
                    AnswerId = x.AnswerId,
                    QuestionName = x.Question,
                    InsertTime = x.InsertTime
                }).ToList();
    }
}