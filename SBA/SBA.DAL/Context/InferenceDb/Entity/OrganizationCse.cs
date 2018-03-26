using System.ComponentModel.DataAnnotations.Schema;

namespace SBA.DAL.Context.InferenceDb.Entity
{
    public class OrganizationCse : ThingCse
    {
        [NotMapped]
        public const string PageMapName = "organization";

        public string Address { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public string Location { get; set; }
        public string Telephone { get; set; }
    }
}