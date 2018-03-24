using SBA.DAL.Context.InferenceDb.Entity;
using System.Data.Entity;

namespace SBA.DAL.Context.InferenceDb.Infrastructure
{
    public class SbaInferenceContext : DbContext
    {
        private static string ContextName => nameof(SbaInferenceContext);

        public DbSet<CseData> CseDatas { get; set; }
        public DbSet<ArticleCse> ArticleCses { get; set; }
        public DbSet<EventCse> EventCses { get; set; }
        public DbSet<OrganizationCse> OrganizationCses { get; set; }
        public DbSet<PersonCse> PersonCses { get; set; }
        public DbSet<VideoCse> VideoCses { get; set; }
        public DbSet<SoftwareApplicationCse> SoftwareApplicationCses { get; set; }
        public DbSet<SoftwareSourceCodeCse> SoftwareSourceCodeCses { get; set; }

        public SbaInferenceContext()
            : base(ContextName)
        { }

        public static SbaInferenceContext Create() =>
            new SbaInferenceContext();
    }
}