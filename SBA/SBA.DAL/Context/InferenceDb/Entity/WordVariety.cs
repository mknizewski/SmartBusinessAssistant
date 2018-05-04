using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBA.DAL.Context.InferenceDb.Entity
{
    public class WordVariety
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(OrginalWord))]
        public int? OrginalWordId { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Index]
        public string Word { get; set; }

        public virtual WordVariety OrginalWord { get; set; }
    }
}