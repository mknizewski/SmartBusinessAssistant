using System.Data.Entity;
using SBA.DAL.Context.WebDb.Entity;

namespace SBA.DAL.Context.WebDb.Infrastructure
{
    public class SbaWebContext : DbContext
    {
        private static string ContextName => "SbaWebContext";

        public DbSet<Configuration> Configurations { get; set; }

        public SbaWebContext() 
            : base(ContextName)
        {
        }

        public static SbaWebContext Create() =>
            new SbaWebContext();
    }
}
