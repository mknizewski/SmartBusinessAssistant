using SBA.DAL.Context.InferenceDb.Entity;
using System.Data.Entity;

namespace SBA.DAL.Context.InferenceDb.Infrastructure
{
    public class SbaInferenceContext : DbContext
    {
        private static string ContextName => nameof(SbaInferenceContext);

        public DbSet<CseData> CseDatas { get; set; }

        public SbaInferenceContext()
            : base(ContextName)
        { }

        public static SbaInferenceContext Create() =>
            new SbaInferenceContext();
    }
}