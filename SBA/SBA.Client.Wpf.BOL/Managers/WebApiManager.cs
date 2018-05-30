using Newtonsoft.Json;
using SBA.BOL.Common.Factory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SBA.Client.Wpf.BOL.Managers
{
    public interface IWebApiManager
    {
        Task<List<Dictionary<string, string>>> GetMessagesAsync();
        void SaveArticleToWebAsync(Dictionary<string, string> dictionary);
    }

    public class WebApiManager : IWebApiManager
    {
        public async Task<List<Dictionary<string, string>>> GetMessagesAsync()
        {
            var httpClient = new HttpClient { BaseAddress = SimpleFactory.Get<Uri>(app.Default.webBasePath) };
            var response = await httpClient.GetAsync(app.Default.messagesApiPath);
            var content = await response.Content.ReadAsStringAsync();
            var jsonDeserializer = JsonSerializer.Create();
            var jsonReader = SimpleFactory.Get<JsonTextReader>(SimpleFactory.Get<StringReader>(content));

            return jsonDeserializer.Deserialize<List<Dictionary<string, string>>>(jsonReader);
        }

        public async void SaveArticleToWebAsync(Dictionary<string, string> dictionary)
        {
            dictionary.Add("AppGuid", app.Default.authGuid);

            var httpClient = new HttpClient { BaseAddress = SimpleFactory.Get<Uri>(app.Default.webBasePath) };
            var response = await httpClient.PostAsJsonAsync(app.Default.saveArticleApiPath, dictionary);
        }
    }
}