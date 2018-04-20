using System.Collections.Generic;

namespace SBA.Client.Wpf.Models
{
    public class FullPath
    {
        public int PathId { get; set; }
        public string FullPathText { get; set; }
    }

    public class PathQuantity
    {
        public int QuantityId { get; set; }
        public int Count { get; set; }
    }

    public class LocalizationForBuilder
    {
        public int ContentId { get; set; }
        public string LocalizationName { get; set; }
    }

    public class BuilderModel
    {
        public int FullPathId { get; set; }

        public int PathQuantityId { get; set; }

        public int LocalizationId { get; set; }

        public virtual FullPath FullPath { get; set; }
        public virtual PathQuantity PathQuantity { get; set; }
        public virtual LocalizationForBuilder LocalizationForBuilder { get; set; }
    }

    public static class BuilderSampleData
    {
        public static List<FullPath> FullPaths { get; set; }
        public static List<PathQuantity> PathQuantities { get; set; }
        public static List<LocalizationForBuilder> LocalizationsForBuilder { get; set; }

        static BuilderSampleData()
        {
            Seed();
        }

        public static void Seed()
        {
            if (FullPaths != null)
                return;

            FullPaths = new List<FullPath>
            {
                new FullPath { FullPathText = "główna.artykułtakiitaki" },
                new FullPath { FullPathText = "główna->o nas." },
                new FullPath { FullPathText = "główna->aktualnościtakaitaka" },
                new FullPath { FullPathText = "główna->kontakt" },
                new FullPath { FullPathText = "główna" },
            };

            PathQuantities = new List<PathQuantity>
            {
                new PathQuantity { Count = 25 },
                new PathQuantity { Count = 100 },
                new PathQuantity { Count = 55 },
                new PathQuantity { Count = 80 },
                new PathQuantity { Count = 120 },
            };

            LocalizationsForBuilder = new List<LocalizationForBuilder>
            {
                new LocalizationForBuilder { LocalizationName = "Warmia" },
                new LocalizationForBuilder { LocalizationName = "Ciechocinek" },
                new LocalizationForBuilder { LocalizationName = "Szuszalewo" },
                new LocalizationForBuilder { LocalizationName = "Żurawlów" },
                new LocalizationForBuilder { LocalizationName = "Białystok" },
            };
        }
    }
}