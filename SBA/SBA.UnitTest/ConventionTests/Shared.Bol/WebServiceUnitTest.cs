using Microsoft.VisualStudio.TestTools.UnitTesting;
using SBA.BOL.Web.Service;
using System.Linq;
using System.Reflection;

namespace SBA.UnitTest.ConventionTests.Shared.Bol
{
    [TestClass]
    public class WebServiceUnitTest
    {
        [TestMethod]
        public void WebService_WhenPrePush_ShouldHaveCorrectNames()
        {
            var sharedBolAssemblyName = typeof(FileService).Assembly.FullName;
            var sharedBolAssembly = Assembly.Load(sharedBolAssemblyName);
            var servicesList = sharedBolAssembly
                .GetTypes()
                .Where(x =>  x.IsClass &&
                            !x.IsNestedPrivate &&
                             x.Namespace.Contains("SBA.BOL.Web.Service"));

            foreach (var service in servicesList)
            {
                var interfaces = service.GetInterfaces();
                bool isHaveCorrectName = service.Name.EndsWith("Service");
                bool isInterfaceHaveCorrectName = interfaces
                    .Any(x => x.Name == $"I{service.Name}");

                Assert.AreEqual<int>(1, interfaces.Length);
                Assert.IsTrue(isHaveCorrectName);
                Assert.IsTrue(isInterfaceHaveCorrectName);
            }
        }
    }
}