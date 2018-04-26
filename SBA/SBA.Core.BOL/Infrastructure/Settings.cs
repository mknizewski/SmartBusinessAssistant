using SBA.Core.BOL.ThreadsSupervisior;
using SBA.DAL.Context.InferenceDb.Infrastructure;

namespace SBA.Core.BOL.Infrastructure
{
    public static class Settings
    {
        internal static core Core => core.Default;
        internal static ThreadSupervisior Supervisior;

        public static void InitDatabase()
        {
            var context = SimpleFactory.Get<SbaInferenceContext>();
            var dbSeed = SimpleFactory.Get<SbaInferenceInitializer>();

            dbSeed.InitializeDatabase(context);
        }
    }
}