using System.ComponentModel.DataAnnotations;

namespace SBA.DAL.Context.InferenceDb.Entity
{
    public class WebLog
    {
        [Key]
        public int Id { get; set; }
        public string SessionId { get; set; }
        public string CurrentTime { get; set; }
        public string ClientIp { get; set; }
        public string CurrentUrl { get; set; }
        public string PreviousUrl { get; set; }
    }
}