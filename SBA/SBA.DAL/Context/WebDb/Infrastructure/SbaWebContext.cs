using System.Data.Entity;
using SBA.DAL.Context.WebDb.Entity;

namespace SBA.DAL.Context.WebDb.Infrastructure
{
    public class SbaWebContext : DbContext
    {
        private static string ContextName => nameof(SbaWebContext);

        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public SbaWebContext() 
            : base(ContextName)
        { }

        public static SbaWebContext Create() =>
            new SbaWebContext();
    }
}