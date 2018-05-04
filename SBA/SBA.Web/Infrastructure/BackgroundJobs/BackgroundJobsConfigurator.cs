using Hangfire;
using SBA.BOL.Web.Service;
using SBA.Web.Infrastructure.Factory;

namespace SBA.Web.Infrastructure.BackgroundJobs
{
    public static class BackgroundJobsConfigurator
    {
        private static BackgroundJobServer _backgroundJobServer;

        public static void Configure()
        {
            GlobalConfiguration
                .Configuration
                .UseSqlServerStorage("SbaWebContext");

            _backgroundJobServer = SimpleFactory.Get<BackgroundJobServer>();
        }

        public static void RegisterBackgroundJobs()
        {
            RecurringJob.AddOrUpdate(() => SimpleFactory.Get<CookieService, ICookieService>().SendLogsToCore(), Cron.Minutely);
        }
    }
}