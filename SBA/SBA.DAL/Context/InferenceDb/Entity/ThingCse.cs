using System.ComponentModel.DataAnnotations.Schema;

namespace SBA.DAL.Context.InferenceDb.Entity
{
    public abstract class ThingCse : BaseSchemaCse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }
}