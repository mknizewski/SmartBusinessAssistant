using System.ComponentModel.DataAnnotations.Schema;

namespace SBA.DAL.Context.InferenceDb.Entity
{
    public class VideoCse : ThingCse
    {
        [NotMapped]
        public const string PageMapName = "videoobject";
    }
}