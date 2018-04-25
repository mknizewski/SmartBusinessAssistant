using System;
using System.ComponentModel.DataAnnotations;

namespace SBA.DAL.Context.InferenceDb.Entity
{
    public class FaqQuestions
    {
        [Key]
        public int Id { get; set; }
        public string Question { get; set; }
        public DateTime InsertTime { get; set; }
    }
}