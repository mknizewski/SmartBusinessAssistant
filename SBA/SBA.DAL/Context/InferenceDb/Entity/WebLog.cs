using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBA.DAL.Context.InferenceDb.Entity
{
    public class WebLog
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(WebSessionId))]
        public int SessionId { get; set; }

        public string CurrentTime { get; set; }

        public string ClientIp { get; set; }

        public string CurrentUrl { get; set; }

        public string PreviousUrl { get; set; }

        public bool IsProcessed { get; set; }

        public virtual WebSessionsId WebSessionId { get; set; }
    }
}