using SBA.BOL.Inference.Service;
using SBA.Core.BOL.Exceptions;
using SBA.Core.BOL.Infrastructure;
using SBA.Core.BOL.Managers;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SBA.Core.BOL.Threads.Socket
{
    public class ServerSocketThread : BaseThread, IThread
    {
        private readonly IServerSocketService _serverSocketService;
        private readonly ILoggerManager _loggerManager;

        public ServerSocketThread()
        {
            _serverSocketService = SimpleFactory.Get<ServerSocketService, IServerSocketService>();
            _loggerManager = SimpleFactory.GetLogger();
        }

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
                try
                {
                    serverSocket.Listen(Settings.Core.MaxConnectionsAllowed);

                    var recvSocket = serverSocket.Accept();
                    var buffer = new byte[recvSocket.ReceiveBufferSize];
                    string ipAddres = (recvSocket.RemoteEndPoint as IPEndPoint).Address.ToString();
                    if (ipAddres != Settings.Core.WebIp)
                        throw new NotAllowedDomainException(ipAddres);

                    string recvLog = $"{nameof(ServerSocketThread)} recived data from WebLayer. [{ipAddres}]";
                    recvSocket.Receive(buffer);
                    
                    _loggerManager.RegisterLogToConsole(recvLog);
                    _loggerManager.RegisterLogToFile(recvLog);

                    string dataToDeliver = _serverSocketService.HandleRecvData(buffer);
                    string sendLog = $"{nameof(ServerSocketThread)} sended data to WebLayer. [{ipAddres}]";

                    recvSocket.Send(Encoding.ASCII.GetBytes(dataToDeliver));
                    _loggerManager.RegisterLogToConsole(sendLog);
                    _loggerManager.RegisterLogToFile(sendLog);
                }
                catch (NotAllowedDomainException ex)
                {
                    _loggerManager.RegisterLogToConsole(ex.Message);
                    _loggerManager.RegisterLogToFile(ex.Message);
                }
            }
        }
    }
}