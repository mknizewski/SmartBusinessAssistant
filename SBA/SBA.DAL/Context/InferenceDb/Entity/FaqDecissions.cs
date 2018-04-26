using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBA.DAL.Context.InferenceDb.Entity
{
    public class FaqDecissions
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(FaqAnswer))]
        public int FaqAnswerId { get; set; }

        public bool Decide { get; set; }

        public double Propability { get; set; }

        public string Classificator { get; set; }

        public virtual FaqAnswers FaqAnswer { get; set; }
    }
}