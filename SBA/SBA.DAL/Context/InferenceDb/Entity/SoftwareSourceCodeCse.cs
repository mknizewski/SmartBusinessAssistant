using System.ComponentModel.DataAnnotations.Schema;

namespace SBA.DAL.Context.InferenceDb.Entity
{
    public class SoftwareSourceCodeCse : CreativeWorkCse
    {
        [NotMapped]
        public const string PageMapName = "softwaresourcecode";

        public string CodeRepository { get; set; }
        public string ProgrammingLanguage { get; set; }
        public string RuntimePlatform { get; set; }
    }
}