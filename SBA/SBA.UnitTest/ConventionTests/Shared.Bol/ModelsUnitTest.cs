using Microsoft.VisualStudio.TestTools.UnitTesting;
using SBA.BOL.Web.Models;
using System.Linq;
using System.Reflection;

namespace SBA.UnitTest.ConventionTests.Shared.Bol
{
    [TestClass]
    public class ModelsUnitTest
    {
        [TestMethod]
        public void WebModels_WhenPrePush_ShouldHaveCorrectNames()
        {
            var sharedBolAssemblyName = typeof(ArticleModel).Assembly.FullName;
            var sharedBolAssembly = Assembly.Load(sharedBolAssemblyName);
            var modelList = sharedBolAssembly
                .GetTypes()
                .Where(x => x.IsClass && x.Namespace.Contains(nameof(BOL.Web.Models)));

            foreach (var model in modelList)
                Assert.IsTrue(model.Name.EndsWith("Model"));
        }

        [TestMethod]
        public void WebModels_WhenPrePush_ShouldHaveOnlyProperties()
        {
            var sharedBolAssemblyName = typeof(ArticleModel).Assembly.FullName;
            var sharedBolAssembly = Assembly.Load(sharedBolAssemblyName);
            var modelList = sharedBolAssembly
                .GetTypes()
                .Where(x => x.IsClass && x.Namespace.Contains(nameof(BOL.Web.Models)));

            foreach (var model in modelList)
            {
                var constructors = model.GetConstructors();
                var properties = model.GetProperties();
                var fields = model.GetFields();
                bool isDefaultConstructorOnly = constructors
                    .Any(x => x.GetParameters().Length == 0);

                Assert.AreEqual<int>(1, constructors.Length);
                Assert.AreNotEqual<int>(0, properties.Length);
                Assert.AreEqual<int>(0, fields.Length);
                Assert.IsTrue(isDefaultConstructorOnly);
            }
        }
    }
}
