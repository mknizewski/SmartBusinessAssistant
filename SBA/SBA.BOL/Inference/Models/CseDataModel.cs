using System;

namespace SBA.BOL.Inference.Models
{
    public class CseDataModel
    {
        public int Id { get; set; }

        public string Query { get; set; }

        public string RawJsonQueryResult { get; set; }

        public string ObjectType { get; set; }

        public DateTime InsertTime { get; set; }
    }
}