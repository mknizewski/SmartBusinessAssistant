using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBA.DAL.Context.InferenceDb.Entity
{
    public class FaqAnswers
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey(nameof(FaqQuestion))]
        public int FaqQuestionId { get; set; }

        public string Answer { get; set; }

        public virtual FaqQuestions FaqQuestion { get; set; }
    }
}