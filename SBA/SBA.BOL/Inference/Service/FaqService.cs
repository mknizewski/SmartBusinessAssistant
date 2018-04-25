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
        FaqModel.Answer GetFaqAnswerByQuestionId(int questionId);
        void AddFaqDecision(FaqModel.Decide faqDecission);
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
                FaqQuestionId = faqDecission.Question.Id,
                Classificator = faqDecission.Classificator
            });

        public FaqModel.Answer GetFaqAnswerByQuestionId(int questionId)
        {
            var dbModel = _faqRespository.GetFaqAnswerByQuestionId(questionId);
            return new FaqModel.Answer
            {
                Id = dbModel.Id,
                AnswerName = dbModel.Answer,
                QuestionId = dbModel.FaqQuestionId
            };
        }

        public List<FaqModel.Question> GetFaqQuestions() =>
            _faqRespository.GetFaqQuestions()
                .Select(x => new FaqModel.Question
                {
                    Id = x.Id,
                    QuestionName = x.Question,
                    InsertTime = x.InsertTime
                }).ToList();
    }
}