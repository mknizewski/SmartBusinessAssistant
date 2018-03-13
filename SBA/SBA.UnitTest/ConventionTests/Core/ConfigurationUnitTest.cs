using Microsoft.VisualStudio.TestTools.UnitTesting;
using SBA.Core.BOL.Infrastructure;
using SBA.Core.BOL.Infrastructure.Configurator;
using SBA.Core.BOL.Threads;
using System.Linq;
using System.Reflection;

namespace SBA.UnitTest.ConventionTests.Core
{
    [TestClass]
    public class ConfigurationUnitTest
    {
        [TestMethod]
        public void Configurator_WhenPrePush_ShouldHaveCorrectNames()
        {
            var coreBolAssemblyName = typeof(Startup).Assembly.FullName;
            var coreBolAssembly = Assembly.Load(coreBolAssemblyName);
            var configuratorClasses = coreBolAssembly
                .GetTypes()
                .Where(x => x.IsClass && x.Namespace.Contains(nameof(SBA.Core.BOL.Infrastructure.Configurator)));

            foreach (var configurator in configuratorClasses)
                Assert.IsTrue(configurator.Name.EndsWith("Configurator"));
        }

        [TestMethod]
        public void Configurator_WhenPrePrush_ShouldHaveImplementInterface()
        {
            var coreBolAssemlbyName = typeof(Startup).Assembly.FullName;
            var coreBolAssembly = Assembly.Load(coreBolAssemlbyName);
            var configuratorClasses = coreBolAssembly
                .GetTypes()
                .Where(x => x.IsClass && x.Namespace.Contains(nameof(SBA.Core.BOL.Infrastructure.Configurator)));

            foreach (var configurator in configuratorClasses)
                Assert.IsTrue(configurator.GetInterfaces().Any(x => x.Name == "IConfigurator`1"));
        }
    }
}