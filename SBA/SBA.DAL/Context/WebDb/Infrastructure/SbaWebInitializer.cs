using SBA.DAL.Context.WebDb.Common;
using SBA.DAL.Context.WebDb.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SBA.DAL.Context.WebDb.Infrastructure
{
    public class SbaWebInitializer : CreateDatabaseIfNotExists<SbaWebContext>
    {
        public override void InitializeDatabase(SbaWebContext context)
        {
            if (!context.Configurations.Any())
            {
                var configurations = new List<Configuration>
                {
                    new Configuration
                    {
                        Key = ConfigurationKeys.ArticleJsonPath,
                        Name = "D:\\SbaWebData\\",
                        InsertTime = DateTime.Now
                    }
                };

                configurations.ForEach(x => context.Configurations.Add(x));
                context.SaveChanges();
            }

            if (!context.Files.Any())
            {
                var files = new List<File>
                {
                    new File
                    {
                        Id = (int) File.Type.ArticleJson,
                        Name = "Artykuł w postaci JSON",
                        InsertTime = DateTime.Now
                    }
                };

                files.ForEach(x => context.Files.Add(x));
                context.SaveChanges();
            }
        }
    }
}
