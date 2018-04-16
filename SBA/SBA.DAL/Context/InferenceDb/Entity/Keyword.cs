using System;
using System.ComponentModel.DataAnnotations;

namespace SBA.DAL.Context.InferenceDb.Entity
{
    public class Keyword
    {
        [Key]
        public int Id { get; set; }
        public string KeywordText { get; set; }
        public int Mark { get; set; }
        public DateTime InsertTime { get; set; }
    }
}