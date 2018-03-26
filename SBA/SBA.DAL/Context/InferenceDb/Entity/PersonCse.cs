using System.ComponentModel.DataAnnotations.Schema;

namespace SBA.DAL.Context.InferenceDb.Entity
{
    public class PersonCse : ThingCse
    {
        [NotMapped]
        public const string PageMapName = "person";

        public string AdditionalName { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public string Nationality { get; set; }
        public string Telephone { get; set; }
    }
}