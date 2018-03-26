using SBA.BOL.Common.Extensions;
using SBA.BOL.Common.Factory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SBA.Core.App.Diag
{
    public class DiagManager
    {
        private static string _coreHost => ConfigurationManager.AppSettings["coreHost"];
        private static string _corePort => ConfigurationManager.AppSettings["corePort"];
        private static string _authGuid => ConfigurationManager.AppSettings["authGuid"];

        public void Process()
        {
            string command = string.Empty;
            while ((command = Console.ReadLine()) != "exit")
                this.HandleCommand(command);
        }

        private void HandleCommand(string command) =>
            Console.WriteLine($"Odp: {this.SendCommandBySocket(command)}");

        private string SendCommandBySocket(string command)
        {
            var ipAddress = IPAddress.Parse(_coreHost);
            var remoteEp = SimpleFactory.Get<IPEndPoint>(ipAddress, Convert.ToInt32(_corePort));
            var socket = SimpleFactory.Get<Socket>(
                ipAddress.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);

            try
            {
                byte[] sendBytes = null;
                byte[] recvBytes = new byte[socket.ReceiveBufferSize];
                var dictionary = this.GetDictionary(command);
                var binaryFormatter = SimpleFactory.Get<BinaryFormatter>();
                using (var memoryStream = SimpleFactory.Get<MemoryStream>())
                {
                    binaryFormatter.Serialize(memoryStream, dictionary);
                    sendBytes = memoryStream.ToArray();
                }

                socket.Connect(remoteEp);
                socket.Send(sendBytes);
                socket.Receive(recvBytes);

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();

                return Encoding.ASCII
                    .GetString(recvBytes)
                    .ClearRecv();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private Dictionary<string, string> GetDictionary(string command) =>
            new Dictionary<string, string>
            {
                { "AuthGuid", _authGuid },
                { "Type", "Diag" },
                { "Command", command }
            };
    }
}