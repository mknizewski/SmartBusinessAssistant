namespace SBA.Client.Wpf.Models
{
    public class RecommendationModel
    {
        public class Tile
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Color { get; set; }
            public string Icon { get; set; }

            public static Tile GetArticle(string id, string name) =>
                new Tile
                {
                    Id = id,
                    Title = name,
                    Color = "DarkOrange",
                    Icon = "Newspaper"
                };

            public static Tile GetEvent(string id, string name) =>
                new Tile
                {
                    Id = id,
                    Title = name,
                    Color = "Green",
                    Icon = "Calendar"
                };

            public static Tile GetOrganization(string id, string name) =>
                new Tile
                {
                    Id = id,
                    Title = name,
                    Color = "IndianRed",
                    Icon = "CabinetFiles"
                };

            public static Tile GetPerson(string id, string name) =>
                new Tile
                {
                    Id = id,
                    Title = name,
                    Color = "Purple",
                    Icon = "People"
                };

            public static Tile GetVideo(string id, string name) =>
                new Tile
                {
                    Id = id,
                    Title = name,
                    Color = "DarkOliveGreen",
                    Icon = "YoutubePlay"
                };
        }
    }
}