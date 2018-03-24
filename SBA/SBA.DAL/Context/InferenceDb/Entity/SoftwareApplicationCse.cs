namespace SBA.DAL.Context.InferenceDb.Entity
{
    public class SoftwareApplicationCse : CreativeWorkCse
    {
        public string ApplicationCategory { get; set; }
        public string AvaiableOnDevice { get; set; }
        public string DownloadURL { get; set; }
        public string FileSize { get; set; }
    }
}