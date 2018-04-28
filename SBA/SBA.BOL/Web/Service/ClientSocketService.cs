using SBA.BOL.Common.Extensions;
using SBA.BOL.Common.Factory;
using SBA.BOL.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SBA.BOL.Web.Service
{
    public interface IClientSocketService
    {
        Dictionary<string, dynamic> SendUserQuestionToGetSuggestAnswer(string userQuestion);
        Task<string> SendHandUp(QuestionModel questionModel);
    }

    public class ClientSocketService : IClientSocketService
    {
        public Task<string> SendHandUp(QuestionModel questionModel) =>
            Task.Run(() =>
            {
                var dictionary = new Dictionary<string, string>
                {
                    { "AuthGuid", ConfigurationManager.AppSettings["authGuid"] },
                    { "Type", "Web" },
                    { "Request", "HandUp" },
                    { "AnswerId", questionModel.AnswerId.ToString() },
                    { "Question", questionModel.Question }
                };

                return Encoding.UTF8
                    .GetString(ExchangeDataWithCore(dictionary))
                    .ClearRecv();
            });

        public Dictionary<string, dynamic> SendUserQuestionToGetSuggestAnswer(string userQuestion)
        {
            var requestDictionary = GetDictionary();
            requestDictionary.Add("Request", "AnswerSuggestion");
            requestDictionary.Add("Question", userQuestion); 

            byte[] coreSuggestion = ExchangeDataWithCore(requestDictionary);
            var binaryFormatter = SimpleFactory.Get<BinaryFormatter>();
            using (var memoryStream = SimpleFactory.Get<MemoryStream>(coreSuggestion))
                return (Dictionary<string, dynamic>) binaryFormatter.Deserialize(memoryStream);
        }

        private byte[] ExchangeDataWithCore(Dictionary<string, string> dictionary)
        {
            byte[] sendBytes = null;
            string coreHost = ConfigurationManager.AppSettings[nameof(coreHost)];
            string corePort = ConfigurationManager.AppSettings[nameof(corePort)];
            var binaryFormatter = SimpleFactory.Get<BinaryFormatter>();
            var clientSocket = SimpleFactory
                .Get<Socket>(
                    AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.IP);

            using (var memoryStream = SimpleFactory.Get<MemoryStream>())
            {
                binaryFormatter.Serialize(memoryStream, dictionary);
                sendBytes = memoryStream.ToArray();
            }

            clientSocket.Connect(coreHost, Convert.ToInt32(corePort));
            clientSocket.Send(sendBytes);

            byte[] serverData = new byte[clientSocket.ReceiveBufferSize];
            clientSocket.Receive(serverData);
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();

            return serverData;
        }

        private Dictionary<string, string> GetDictionary() =>
            new Dictionary<string, string>
            {
                { "AuthGuid", ConfigurationManager.AppSettings["authGuid"] },
                { "Type", "Web" }
            };
    }
}