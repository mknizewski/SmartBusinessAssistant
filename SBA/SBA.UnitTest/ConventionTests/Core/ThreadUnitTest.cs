using Microsoft.VisualStudio.TestTools.UnitTesting;
using SBA.Core.BOL.Infrastructure;
using SBA.Core.BOL.Threads;
using System.Linq;
using System.Reflection;

namespace SBA.UnitTest.ConventionTests.Core
{
    [TestClass]
    public class ThreadUnitTest
    {
        [TestMethod]
        public void Threads_WhenPrePush_ShouldHaveCorrectNames()
        {
            var coreBolAssemblyName = typeof(Startup).Assembly.FullName;
            var coreBolAssembly = Assembly.Load(coreBolAssemblyName);
            var threadClasses = coreBolAssembly
                .GetTypes()
                .Where(x => x.IsClass && 
                            x.Namespace.Contains($"{nameof(SBA.Core.BOL.Threads)}.") &&
                            x.Name != "ExcecutionPlan");

            foreach (var thread in threadClasses)
                Assert.IsTrue(thread.Name.EndsWith("Thread"));
        }

        [TestMethod]
        public void Threads_WhenPrePush_ShouldImplementAndInheritCorrectTypes()
        {
            var coreBolAssemblyName = typeof(Startup).Assembly.FullName;
            var coreBolAssembly = Assembly.Load(coreBolAssemblyName);
            var threadClasses = coreBolAssembly
                .GetTypes()
                .Where(x => x.IsClass &&
                            x.Namespace.Contains($"{nameof(SBA.Core.BOL.Threads)}.") &&
                            x.Name != "ExcecutionPlan");

            foreach (var thread in threadClasses)
            {
                bool isNested = thread.IsSubclassOf(typeof(BaseThread));
                bool isImplementInterface = thread
                    .GetInterfaces()
                    .Contains(typeof(IThread));

                Assert.IsTrue(isNested);
                Assert.IsTrue(isImplementInterface);
            }
        }
    }
}