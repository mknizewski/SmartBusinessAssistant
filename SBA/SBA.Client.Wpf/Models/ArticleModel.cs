using System.Collections.Generic;

namespace SBA.Client.Wpf.Models
{
    public class Title
    {
        public int TitleId { get; set; }
        public string TitleName { get; set; }
    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class Content
    {
        public int ContentId { get; set; }
        public string ContentText { get; set; }
    }

    public class SourceOfArticle
    {
        public int SourceId { get; set; }
        public string SourceText { get; set; }
    }

    public class ArticleModel
    {
        public int TitleId { get; set; }

        public int CategoryId { get; set; }

        public int ContentId { get; set; }

        public int SourceId { get; set; }

        public virtual Title Title { get; set; }
        public virtual Category Category { get; set; }
        public virtual Content Content { get; set; }
        public virtual SourceOfArticle SourceOfArticle { get; set; }
    }

    public static class ArticleSampleData
    {
        public static List<Title> Titles { get; set; }
        public static List<Category> Categories { get; set; }
        public static List<Content> Contents { get; set; }
        public static List<SourceOfArticle> Sources { get; set; }
        public static List<ArticleModel> Articles { get; set; }

        static ArticleSampleData()
        {
            Seed();
        }

        public static void Seed()
        {
            if (Categories != null)
                return;

            Titles = new List<Title>
            {
                new Title { TitleName = "Wykład o dotnecie" },
                new Title { TitleName = "Meeting programistyczny" },
                new Title { TitleName = "Nowinki 2018" },
                new Title { TitleName = "Targi na zakup sprzętu" },
                new Title { TitleName = "Kurs programowania" },
            };

            Categories = new List<Category>
            {
                new Category { CategoryName = "Programowanie" },
                new Category { CategoryName = "Znani ludzie" },
                new Category { CategoryName = "Informatyka" },
                new Category { CategoryName = "Bizness" },
                new Category { CategoryName = "Sprzęt programistyczny" },
            };

            Contents = new List<Content>
            {
                new Content { ContentText = "Jakaś treść 1..." },
                new Content { ContentText = "Jakaś treść 2..." },
                new Content { ContentText = "Jakaś treść 3..." },
                new Content { ContentText = "Jakaś treść 4..." },
                new Content { ContentText = "Jakaś treść 5..." },
            };

            Sources = new List<SourceOfArticle>
            {
                new SourceOfArticle { SourceText = "www.dotnet.pl"},
                new SourceOfArticle { SourceText = "www.biznesszklasa.pl"},
                new SourceOfArticle { SourceText = "www.meetingjs.pl"},
                new SourceOfArticle { SourceText = "www.destroyjava.pl"},
                new SourceOfArticle { SourceText = "www.cheaphardware.pl"}
            };
        }
    }
}