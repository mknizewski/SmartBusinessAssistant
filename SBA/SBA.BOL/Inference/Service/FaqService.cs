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
        void AddFaqDecision(FaqModel.Decide faqDecission);
        void AddFaqDecisions(List<FaqModel.Decide> decides);
        string GetAnswer(int answerId);
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

        public string GetAnswer(int answerId)
        {
            var answer = _faqRespository.GetAnswer(answerId);
            return answer.Answer;
        }

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