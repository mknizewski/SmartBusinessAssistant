using Accord.MachineLearning;
using Accord.Statistics.Models.Regression;
using Accord.Statistics.Models.Regression.Fitting;
using SBA.BOL.Inference.Models;
using SBA.BOL.Inference.Service;
using SBA.Core.BOL.Common.Extensions;
using SBA.Core.BOL.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBA.Core.BOL.Threads.FaqAnswerAdjusting
{
    public class FaqAnswerAdjustingThread : BaseThread, IThread
    {
        private readonly IFaqService _faqService;
        private readonly IWordVarietyService _wordVarietyService;

        public FaqAnswerAdjustingThread()
        {
            _faqService = SimpleFactory.Get<FaqService, IFaqService>();
            _wordVarietyService = SimpleFactory.Get<WordVarietyService, IWordVarietyService>();
        }

        public override T DoJob<T>()
        {
            string userQuestion = ExcecutionPlan.Parameters[0];
            var decides = SimpleFactory.Get<List<FaqModel.Decide>>();
            var faqQuestions = _faqService.GetFaqQuestions();

            var terms = new TFIDF()
            {
                Tf = TermFrequency.Log,
                Idf = InverseDocumentFrequency.Max
            };

            var tokenized = faqQuestions
                .Select(x => new StringMap
                {
                    Tokenized = _wordVarietyService.Lemmatize(x.QuestionName.Tokenize().ExcludeStopWords(StopWordLanguage.Polish)),
                    AnswerId = x.AnswerId
                }).ToList();

            terms.Learn(tokenized.Select(x => x.Tokenized).ToArray());
            double[] userInput = terms.Transform(
                _wordVarietyService.Lemmatize(
                    userQuestion
                        .Tokenize()
                        .ExcludeStopWords(StopWordLanguage.Polish)));

            Parallel.ForEach(faqQuestions, answer =>
            {
                var vectors = SimpleFactory.Get<List<VectorMap>>();
                var tokensByAnswer = tokenized
                    .Where(x => x.AnswerId == answer.AnswerId);

                foreach (var token in tokensByAnswer)
                    vectors.Add(new VectorMap
                    {
                        Input = terms.Transform(token.Tokenized),
                        Decide = true
                    });

                vectors.Add(new VectorMap
                {
                    Input = new double[vectors.First().Input.Length],
                    Decide = false
                });

                var learner = new IterativeReweightedLeastSquares<LogisticRegression>()
                {
                    Tolerance = 1e-6,
                    MaxIterations = 100,
                    Regularization = 0
                };

                var logisticRegression = learner.Learn(
                    vectors.Select(x => x.Input).ToArray(),
                    vectors.Select(x => x.Decide).ToArray());

                bool decission = logisticRegression.Decide(userInput);
                double propabality = logisticRegression.Probability(userInput, out decission);

                decides.Add(new FaqModel.Decide
                {
                    AnswerId = answer.AnswerId,
                    Question = userQuestion,
                    DecideStatus = decission,
                    Propability = propabality > 0.0 ? propabality : 0.0,
                    Classificator = nameof(LogisticRegression)
                });
            });

            Task.Run(() => _faqService.AddFaqDecisions(decides));
            return decides as T;
        }

        private struct VectorMap
        {
            public double[] Input { get; set; }
            public bool Decide { get; set; }
        }

        private struct StringMap
        {
            public string[] Tokenized { get; set; }
            public int AnswerId { get; set; }
        }
    }
}