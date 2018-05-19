namespace SBA.Client.Wpf.Models
{
    public class FaqModel
    {
        public class Question
        {
            public string Id { get; set; }
            public string QuestionName { get; set; }
            public string AnswerId { get; set; }
            public string InsertTime { get; set; }
        }

        public class Answer
        {
            public string Id { get; set; }
            public string AnswerName { get; set; }
        }

        public class New
        {
            public string Question { get; set; }
            public string AnswerId { get; set; }
        }
    }
}