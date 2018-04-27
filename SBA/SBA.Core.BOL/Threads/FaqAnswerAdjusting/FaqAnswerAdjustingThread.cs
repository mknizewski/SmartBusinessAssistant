using Accord.MachineLearning;
using Accord.Statistics.Models.Regression;
using Accord.Statistics.Models.Regression.Fitting;
using SBA.BOL.Inference.Models;
using SBA.BOL.Inference.Service;
using SBA.Core.BOL.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace SBA.Core.BOL.Threads.FaqAnswerAdjusting
{
    public class FaqAnswerAdjustingThread : BaseThread, IThread
    {
        private readonly IFaqService _faqService;

        public FaqAnswerAdjustingThread() => 
            _faqService = SimpleFactory.Get<FaqService, IFaqService>();

        public override T DoJob<T>()
        {
            string userQuestion = ExcecutionPlan.Parameters[0];
            var decides = SimpleFactory.Get<List<FaqModel.Decide>>();
            var questionGroupByAnswers = _faqService
                .GetFaqQuestions()
                .GroupBy(x => x.AnswerId);
            var terms = new TFIDF()
            {
                Tf = TermFrequency.Log,
                Idf = InverseDocumentFrequency.Default
            };
            string[][] tokenized = _faqService
                .GetFaqQuestions()
                .Select(x => x.QuestionName)
                .ToArray()
                .Tokenize();

            terms.Learn(tokenized);
            foreach (var answer in questionGroupByAnswers)
            {
                var vectors = SimpleFactory.Get<List<VectorMap>>();
                foreach (var token in answer)
                    vectors.Add(new VectorMap
                    {
                        Input = terms.Transform(token.QuestionName.Tokenize()),
                        Decide = true
                    });
                
                vectors.Add(new VectorMap
                {
                    Input = new double[vectors.First().Input.Length],
                    Decide = false
                });

                var learner = new IterativeReweightedLeastSquares<LogisticRegression>()
                {
                    Tolerance = 1e-4,
                    MaxIterations = 100,
                    Regularization = 0
                };

                var logisticRegression = learner.Learn(
                    vectors.Select(x => x.Input).ToArray(),
                    vectors.Select(x => x.Decide).ToArray());

                double[] userInput = terms.Transform(userQuestion.Tokenize());
                bool decission = logisticRegression.Decide(userInput);
                double propabality = logisticRegression.Probability(userInput, out decission);

                decides.Add(new FaqModel.Decide
                {
                    AnswerId = answer.Key,
                    Answer = _faqService.GetAnswer(answer.Key),
                    Question = userQuestion,
                    DecideStatus = decission,
                    Propability = propabality,
                    Classificator = nameof(LogisticRegression)
                });
            }

            _faqService.AddFaqDecisions(decides);
            return decides as T;
        }

        private struct VectorMap
        {
            public double[] Input { get; set; }
            public bool Decide { get; set; }
        }
    }
}