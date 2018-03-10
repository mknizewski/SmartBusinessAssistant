using SBA.Core.BOL.Threads;
using SBA.Core.BOL.Threads.Socket;
using System;

namespace SBA.Core.BOL.Infrastructure.Configurator.Threads
{
    public class ServerSocketConfigurator : IConfigurator<ServerSocketThread>
    {
        public ServerSocketThread Configure() => new ServerSocketThread
        {
            ExcecutionPlan = new ExcecutionPlan
            {
                ThreadName = nameof(ServerSocketThread),
                ExecuteTime = TimeSpan.FromSeconds(5.0),
                WorkAsSingleton = true,
                RunManually = false
            }
        };
    }
}