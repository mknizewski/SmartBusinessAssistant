namespace SBA.DAL.Context.InferenceDb.Entity
{
    public class SoftwareSourceCodeCse : CreativeWorkCse
    {
        public string CodeRepository { get; set; }
        public string ProgrammingLanguage { get; set; }
        public string RuntimePlatform { get; set; }
    }
}