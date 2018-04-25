using System;

namespace SBA.BOL.Inference.Models
{
    public class FaqModel
    {
        public class Question
        {
            public int Id { get; set; }
            public string QuestionName { get; set; }
            public DateTime InsertTime { get; set; }
        }

        public class Answer
        {
            public int Id { get; set; }
            public string AnswerName { get; set; }
            public int QuestionId { get; set; }
        }

        public class Decide
        {
            public bool DecideStatus { get; set; }

            public double Propability { get; set; }

            public string Classificator { get; set; }

            public Question Question { get; set; }
        }
    }
}