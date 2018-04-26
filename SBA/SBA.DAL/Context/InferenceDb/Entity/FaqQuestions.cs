using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBA.DAL.Context.InferenceDb.Entity
{
    public class FaqQuestions
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(FaqAnswer))]
        public int AnswerId { get; set; }

        public string Question { get; set; }

        public DateTime InsertTime { get; set; }

        public virtual FaqAnswers FaqAnswer { get; set; }
    }
}