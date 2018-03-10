using System;

namespace SBA.Core.BOL.Threads
{
    public class ExcecutionPlan
    {
        public string ThreadName { get; set; }
        public TimeSpan ExecuteTime { get; set; }
        public DateTime LastExecuteTime { get; set; }

        /// <summary>
        /// Zmienna określająca czy instancje jobów mogą być wielokrotne
        /// Aktualnie każdy chodzi jako singleton lecz jeśli najdzie potrzeba wtedy funkcjonalność zaimplementować
        /// TODO: Podjąć decyzje do wdrożenia + komentarz do usunięcia w trakcie wdrożenia na środowisko testowe
        /// </summary>
        public bool WorkAsSingleton { get; set; }
        public bool RunManually { get; set; }

        public static class PreThreadTime
        {
            public static TimeSpan OneMinute => TimeSpan.FromMinutes(1.0);
            public static TimeSpan FiveMinutes => TimeSpan.FromMinutes(5.0);
            public static TimeSpan Quarter => TimeSpan.FromMinutes(15.0);
            public static TimeSpan HalfHour => TimeSpan.FromMinutes(30.0);
            public static TimeSpan Hour => TimeSpan.FromHours(1.0);
            public static TimeSpan Day => TimeSpan.FromDays(1.0);
            public static TimeSpan Week => TimeSpan.FromDays(7.0);
        }
    }
}