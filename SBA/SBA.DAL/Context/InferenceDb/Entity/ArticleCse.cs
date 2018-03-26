using System.ComponentModel.DataAnnotations.Schema;

namespace SBA.DAL.Context.InferenceDb.Entity
{
    public class ArticleCse : CreativeWorkCse
    {
        [NotMapped]
        public const string PageMapName = "article";

        public string ArticleBody { get; set; }
        public string ArticleSection { get; set; }
    }
}