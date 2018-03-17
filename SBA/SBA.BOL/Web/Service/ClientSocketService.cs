using SBA.BOL.Common.Factory;
using System;
using System.Configuration;
using System.Net.Sockets;
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
            string coreHost = ConfigurationManager.AppSettings[nameof(coreHost)];
            string corePort = ConfigurationManager.AppSettings[nameof(corePort)];
            var clientSocket = SimpleFactory
                .Get<Socket>(
                    AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.IP);

            clientSocket.Connect(coreHost, Convert.ToInt32(corePort));
            clientSocket.Send(Encoding.ASCII.GetBytes(data));

            byte[] serverData = new byte[clientSocket.ReceiveBufferSize];
            clientSocket.Receive(serverData);

            return Encoding.ASCII.GetString(serverData).Replace("\0", string.Empty);
        }
    }
}