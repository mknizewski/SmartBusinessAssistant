using System.Collections.Generic;

namespace SBA.BOL.Web.Models
{
    public class ArticleModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string InsertTime { get; set; }
    }

    public class ArticlesModel
    {
        public IEnumerable<ArticleModel> Articles { get; set; }
        public int Page { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
