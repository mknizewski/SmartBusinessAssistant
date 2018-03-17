using System;

namespace SBA.Core.BOL.Threads.GarbageCollector
{
    public class GarbageCollectorThread : BaseThread, IThread
    {
        public override void DoJob(params string[] jobParams) => 
            GC.Collect();
    }
}