namespace SBA.BOL.Web.Models
{
    public class WebLogModel
    {
        public int SessionId { get; set; }
        public string CurrentTime { get; set; }
        public string ClientIp { get; set; }
        public string CurrentUrl { get; set; }
        public string PreviousUrl { get; set; }
    }
}