using Hangfire;
using System;
using System.Diagnostics;

namespace SBA.Web.Infrastructure.BackgroundJobs
{
    public static class BackgroundJobsConfigurator
    {
        public static void Configure()
        {
            GlobalConfiguration
                .Configuration
                .UseSqlServerStorage("SbaWebContext");
        }

        public static void RegisterBackgroundJobs()
        {
            BackgroundJob.Schedule(() => Console.Write("BackgroundJobsTest"), TimeSpan.FromSeconds(5.0));
        }
    }
}