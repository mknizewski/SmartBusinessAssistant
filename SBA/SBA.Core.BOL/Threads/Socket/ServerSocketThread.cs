using SBA.BOL.Inference.Service;
using SBA.Core.BOL.Infrastructure;
using System.Net;
using System.Net.Sockets;

namespace SBA.Core.BOL.Threads.Socket
{
    public class ServerSocketThread : BaseThread, IThread
    {
        private readonly IServerSocketService _serverSocketService;

        public ServerSocketThread() =>
            _serverSocketService = SimpleFactory.Get<ServerSocketService, IServerSocketService>();

        public override void DoJob()
        {
            var isListing = true;
            var serverSocket = SimpleFactory
                .Get<System.Net.Sockets.Socket>(
                    AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.IP);
            
            serverSocket.Bind(SimpleFactory.Get<IPEndPoint>(
                IPAddress.Parse(Settings.Core.SocketServerIp),
                Settings.Core.SocketServerPort));

            while (isListing)
            {
                serverSocket.Listen(Settings.Core.MaxConnectionsAllowed);

                var recvSocket = serverSocket.Accept();
                var buffer = new byte[recvSocket.ReceiveBufferSize];

                recvSocket.Receive(buffer);
                _serverSocketService.HandleRecvData(buffer);
            }
        }
    }
}