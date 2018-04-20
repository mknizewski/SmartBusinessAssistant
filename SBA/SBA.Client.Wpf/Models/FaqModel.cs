using System.Collections.Generic;

namespace SBA.Client.Wpf.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string FullQuestionText { get; set; }
    }

    public class SimilarQuestionQuantity
    {
        public int QuantityId { get; set; }
        public int Count { get; set; }
    }

    public class Usefulness
    {
        public int UsefulnessId { get; set; }
        public int Rank { get; set; }
    }

    public class LocalizationForFaq
    {
        public int ContentId { get; set; }
        public string LocalizationName { get; set; }
    }

    public class FaqModel
    {
        public int QuestionId { get; set; }

        public int QuestionQuantityId { get; set; }

        public int UsefulnessId { get; set; }

        public int LocalizationId { get; set; }

        public virtual Question Question { get; set; }
        public virtual SimilarQuestionQuantity SimilarQuestionQuantity { get; set; }
        public virtual Usefulness Usefulness { get; set; }
        public virtual LocalizationForFaq LocalizationForFaq { get; set; }
    }

    public static class FaqSampleData
    {
        public static List<Question> Questions { get; set; }
        public static List<SimilarQuestionQuantity> SimilarQuestionQuantities { get; set; }
        public static List<Usefulness> Usefulnesses { get; set; }
        public static List<LocalizationForFaq> LocalizationsForFaq { get; set; }

        static FaqSampleData()
        {
            Seed();
        }

        public static void Seed()
        {
            if (Questions != null)
                return;

            Questions = new List<Question>
            {
                new Question { FullQuestionText = "Czym się zajmujemy?" },
                new Question { FullQuestionText = "Jaka jest lokalizacja firmy?" },
                new Question { FullQuestionText = "Jakie są ceny za konkretne usługi?" },
                new Question { FullQuestionText = "Największe zrealizowane projekty?" },
                new Question { FullQuestionText = "Ile lat istniejemy na rynku?" },
            };

            SimilarQuestionQuantities = new List<SimilarQuestionQuantity>
            {
                new SimilarQuestionQuantity { Count = 5 },
                new SimilarQuestionQuantity { Count = 10 },
                new SimilarQuestionQuantity { Count = 15 },
                new SimilarQuestionQuantity { Count = 20 },
                new SimilarQuestionQuantity { Count = 25 },
            };

            Usefulnesses = new List<Usefulness>
            {
                new Usefulness { Rank = 1 },
                new Usefulness { Rank = 2 },
                new Usefulness { Rank = 3 },
                new Usefulness { Rank = 4 },
                new Usefulness { Rank = 5 },
            };

            LocalizationsForFaq = new List<LocalizationForFaq>
            {
                new LocalizationForFaq { LocalizationName = "Warmia" },
                new LocalizationForFaq { LocalizationName = "Ciechocinek" },
                new LocalizationForFaq { LocalizationName = "Szuszalewo" },
                new LocalizationForFaq { LocalizationName = "Żurawlów" },
                new LocalizationForFaq { LocalizationName = "Białystok" },
            };
        }
    }
}