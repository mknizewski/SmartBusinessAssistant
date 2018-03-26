using SBA.BOL.Common.Extensions;
using SBA.BOL.Common.Factory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SBA.BOL.Web.Service
{
    public interface IClientSocketService
    {
        string ExchangeDataWithCore(string data);
    }

    public class ClientSocketService : IClientSocketService
    {
        public string ExchangeDataWithCore(string data)
        {
            byte[] sendBytes = null;
            string coreHost = ConfigurationManager.AppSettings[nameof(coreHost)];
            string corePort = ConfigurationManager.AppSettings[nameof(corePort)];
            var binaryFormatter = SimpleFactory.Get<BinaryFormatter>();
            var dictionary = GetDictionary(data);
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

            return Encoding.ASCII
                .GetString(serverData)
                .ClearRecv();
        }

        private Dictionary<string, string> GetDictionary(string data) =>
            new Dictionary<string, string>
            {
                { "AuthGuid", ConfigurationManager.AppSettings["authGuid"] },
                { "Type", "Web" },
                { "Data", data}
            };
    }
}