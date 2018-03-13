using Microsoft.VisualStudio.TestTools.UnitTesting;
using SBA.DAL.Context.WebDb.Repository;
using System.Linq;
using System.Reflection;

namespace SBA.UnitTest.ConventionTests.Shared.Dal
{
    [TestClass]
    public class WebRepositoryUnitTest
    {
        [TestMethod]
        public void WebRepository_WhenPrePush_ShouldHaveCorrectNames()
        {
            var sharedDalAssemblyName = typeof(BaseRepository).Assembly.FullName;
            var sharedDalAssembly = Assembly.Load(sharedDalAssemblyName);
            var repositoryList = sharedDalAssembly
                .GetTypes()
                .Where(x => x.IsClass &&
                           !x.IsNestedPrivate &&
                            x.Namespace.Contains("SBA.DAL.Context.WebDb.Repository"));

            foreach (var repository in repositoryList)
                Assert.IsTrue(repository.Name.EndsWith("Repository"));
        }

        [TestMethod]
        public void WebRepository_WhenPrePush_ShouldHaveImplementCorrectInterface()
        {
            var sharedDalAssemblyName = typeof(BaseRepository).Assembly.FullName;
            var sharedDalAssembly = Assembly.Load(sharedDalAssemblyName);
            var repositoryList = sharedDalAssembly
                .GetTypes()
                .Where(x => x.IsClass &&
                           !x.IsNestedPrivate &&
                            x.Name != "BaseRepository" &&
                            x.Namespace.Contains("SBA.DAL.Context.WebDb.Repository"));

            foreach (var repository in repositoryList)
            {
                var interfaces = repository.GetInterfaces();
                bool isCorrectInterface = interfaces
                    .Any(x => x.Name == $"I{repository.Name}");

                Assert.AreEqual<int>(2, interfaces.Length);
                Assert.IsTrue(isCorrectInterface);
            }
        }
    }
}