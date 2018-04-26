using System.ComponentModel.DataAnnotations;

namespace SBA.DAL.Context.InferenceDb.Entity
{
    public class FaqAnswers
    {
        [Key]
        public int Id { get; set; }

        public string Answer { get; set; }
    }
}