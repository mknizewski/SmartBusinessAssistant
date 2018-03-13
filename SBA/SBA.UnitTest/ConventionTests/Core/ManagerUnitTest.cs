using Microsoft.VisualStudio.TestTools.UnitTesting;
using SBA.Core.BOL.Infrastructure;
using System.Linq;
using System.Reflection;

namespace SBA.UnitTest.ConventionTests.Core
{
    [TestClass]
    public class ManagerUnitTest
    {
        [TestMethod]
        public void Manager_WhenPrePush_ShouldContainsCorrectNames()
        {
            var coreBolAssemblyName = typeof(Startup).Assembly.FullName;
            var coreBolAssembly = Assembly.Load(coreBolAssemblyName);
            var managerClasses = coreBolAssembly
                .GetTypes()
                .Where(x => x.IsClass && x.Namespace.Contains(nameof(SBA.Core.BOL.Managers)));

            foreach (var manager in managerClasses)
            {
                var interfaces = manager.GetInterfaces();
                bool isEndsWithManager = manager.Name.EndsWith("Manager");
                bool isInterfaceHaveCorrectName = interfaces
                    .Any(x => x.Name.StartsWith("I") &&
                              x.Name.EndsWith("Manager"));

                Assert.IsTrue(isEndsWithManager);
                Assert.AreEqual<int>(1, interfaces.Length);
                Assert.IsTrue(isInterfaceHaveCorrectName);
            }
        }
    }
}