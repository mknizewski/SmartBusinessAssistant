using SBA.Core.BOL.Infrastructure;
using System;

namespace SBA.Core.BOL.Threads.GarbageCollector
{
    public class GarbageCollectorThread : BaseThread, IThread
    {
        public override T DoJob<T>()
        {
            GC.Collect();
            return Nothing.All as T;
        }
    }
}