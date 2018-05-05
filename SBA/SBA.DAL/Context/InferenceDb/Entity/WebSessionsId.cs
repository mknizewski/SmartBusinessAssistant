using System.ComponentModel.DataAnnotations;

namespace SBA.DAL.Context.InferenceDb.Entity
{
    public class WebSessionsId
    {
        [Key]
        public int Id { get; set; }
        public string SessionId { get; set; }
    }
}