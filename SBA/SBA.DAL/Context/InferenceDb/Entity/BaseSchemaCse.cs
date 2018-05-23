using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBA.DAL.Context.InferenceDb.Entity
{
    public abstract class BaseSchemaCse
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(CseData))]
        public int CseDataId { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public string DisplayLink { get; set; }

        public string Snippet { get; set; }

        public DateTime InsertTime { get; set; }

        public bool IsShowed { get; set; }

        public bool IsFavorite { get; set; }

        public virtual CseData CseData { get; set; }
    }
}