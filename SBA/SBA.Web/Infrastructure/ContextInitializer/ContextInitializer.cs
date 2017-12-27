using SBA.DAL.Context.WebDb.Infrastructure;
using SBA.Web.Infrastructure.Factory;

namespace SBA.Web.Infrastructure.ContextInitializer
{
    public static class ContextInitializer
    {
        public static class SbaWebInitializer
        {
            public static void Init()
            {
                var dbContext = SimpleFactory.CreateInstance<SbaWebContext>();
                var dbInitialzer = SimpleFactory.CreateInstance<DAL.Context.WebDb.Infrastructure.SbaWebInitializer>();

                dbInitialzer.InitializeDatabase(dbContext);
            }
        }
    }
}