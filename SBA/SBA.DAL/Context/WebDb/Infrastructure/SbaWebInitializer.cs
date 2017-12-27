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
                        Key = "InitlalKey",
                        Name = "InitialValue",
                        InsertTime = DateTime.Now
                    }
                };

                configurations.ForEach(x => context.Configurations.Add(x));
                context.SaveChanges();
            }
        }
    }
}
