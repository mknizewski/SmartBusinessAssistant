using Caliburn.Micro;

namespace SBA.Client.Wpf.ViewModels
{
    public class ArticleViewModel : Screen
    {
        public string ArticleTitle { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }
        public string SourceOfArticle { get; set; }
    }
}