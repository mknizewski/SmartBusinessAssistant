using System;
using System.ComponentModel.DataAnnotations;

namespace SBA.DAL.Context.InferenceDb.Entity
{
    public class CseData
    {
        [Key]
        public int Id { get; set; }

        public string Query { get; set; }

        public string RawJsonQueryResult { get; set; }

        public DateTime InsertTime { get; set; }
    }
}