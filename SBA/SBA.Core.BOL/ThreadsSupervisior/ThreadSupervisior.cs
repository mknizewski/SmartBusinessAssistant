﻿using SBA.Core.BOL.Infrastructure;
using SBA.Core.BOL.Infrastructure.Configurator;
using SBA.Core.BOL.Threads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SBA.Core.BOL.ThreadsSupervisior
{
    public class ThreadSupervisior
    {
        private List<BaseThread> _threads;
        private ThreadWorkflow _threadWorkflow;
        private bool _supervising = true;

        private ThreadSupervisior()
        {
            _threads = SimpleFactory.Get<List<BaseThread>>();
            _threadWorkflow = SimpleFactory.Get<ThreadWorkflow>();
        }

        public static ThreadSupervisior GetSupervisior() => 
            new ThreadSupervisior();

        public ThreadSupervisior RegisterThreads()
        {
            var configurators = Assembly
                    .GetCallingAssembly()
                    .GetTypes()
                    .Where(x => x.GetInterfaces().Any(i => i.IsGenericType && i.Name.Contains("Configurator")));

            foreach (var configurator in configurators)
            {
                var configuratorInstance = Activator.CreateInstance(configurator);
                var thread = (BaseThread) configuratorInstance
                    .GetType()
                    .GetMethod(nameof(IConfigurator<BaseThread>.Configure))
                    .Invoke(configuratorInstance, null);

                _threads.Add(thread);
            }

            return this;
        }

        public void Supervise()
        {
            while (_supervising)
                _threadWorkflow.CheckJobs(_threads);
        }
    }
}