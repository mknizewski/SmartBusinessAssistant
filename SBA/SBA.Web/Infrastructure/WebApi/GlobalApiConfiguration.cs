using SBA.BOL.Common.Factory;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace SBA.Web.Infrastructure.WebApi
{
    public static class GlobalApiConfiguration
    {
        public static void ConfigureMediaTypes(this HttpConfiguration configuration)
        {
            configuration.Formatters.Clear();
            configuration.Formatters.Add(SimpleFactory.Get<JsonMediaTypeFormatter>());
        }
    }
}