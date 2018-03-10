using SBA.Core.BOL.Threads;
using SBA.Core.BOL.Threads.GarbageCollector;

namespace SBA.Core.BOL.Infrastructure.Configurator.GarbageCollector
{
    /// <summary>
    /// Job do wymuszania zbierania śmieci
    /// Aktualnie nie przeprowadzono testów czy appka core będzie się "zapychać"
    /// Istnieje podjerzenie za skonczone joby mogą miec jeszcze alokację w pamięci, testy przeprowadzić
    /// Jezeli będzie potrzeba uruchamiania tego joba, ustalic częstotliwość
    /// TODO: Podjąć decyzję jak przeprowadzić test, usunąć komentarz do wdrożenia
    /// </summary>
    public class GarbageCollectorConfigurator : IConfigurator<GarbageCollectorThread>
    {
        public GarbageCollectorThread Configure() => new GarbageCollectorThread
        {
            ExcecutionPlan = new ExcecutionPlan
            {
                ThreadName = nameof(GarbageCollectorThread),
                ExecuteTime = ExcecutionPlan.PreThreadTime.OneMinute,
                WorkAsSingleton = true,
                RunManually = false
            }
        };
    }
}