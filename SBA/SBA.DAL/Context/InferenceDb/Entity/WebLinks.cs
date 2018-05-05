using System.ComponentModel.DataAnnotations;

namespace SBA.DAL.Context.InferenceDb.Entity
{
    public class WebLinks
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
    }
}