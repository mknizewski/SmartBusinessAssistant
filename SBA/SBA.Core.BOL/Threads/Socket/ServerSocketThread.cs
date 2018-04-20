using SBA.BOL.Inference.Service;
using SBA.Core.BOL.Infrastructure;
using SBA.Core.BOL.Managers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SBA.Core.BOL.Threads.Socket
{
    public class ServerSocketThread : BaseThread, IThread
    {
        private readonly IServerSocketService _serverSocketService;
        private readonly ILoggerManager _loggerManager;
        private readonly IDiagnosticManager _diagnosticManager;
        private readonly IConnectionHandler _connectionHandler;
        private static readonly string[] _acceptedAuthGuids =
        {
            Settings.Core.WebAuthGuid,
            Settings.Core.DiagAuthGuid,
            Settings.Core.ClientAuthGuid
        };

        public ServerSocketThread()
        {
            _serverSocketService = SimpleFactory.Get<ServerSocketService, IServerSocketService>();
            _loggerManager = SimpleFactory.GetLogger();
            _diagnosticManager = SimpleFactory.Get<DiagnosticManager, IDiagnosticManager>();
            _connectionHandler = SimpleFactory.Get<ConnectionHandler, IConnectionHandler>();
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

            serverSocket.Listen(Settings.Core.MaxConnectionsAllowed);

            while (isListing)
            {
                byte[] sendBytes = null;
                var socketHandler = serverSocket.Accept();

                try
                {
                    byte[] recvBytes = new byte[socketHandler.ReceiveBufferSize];

                    _loggerManager.RegisterLogToConsole($"Incoming connection to: {nameof(ServerSocketThread)} from: {socketHandler.RemoteEndPoint.ToString()}");
                    _loggerManager.RegisterLogToFile($"Incoming connection to: {nameof(ServerSocketThread)} from: {socketHandler.RemoteEndPoint.ToString()}");
                    socketHandler.Receive(recvBytes);

                    var dictionary = _serverSocketService.DeserializeDictionary(recvBytes);
                    _serverSocketService.AuthorizeConnection(dictionary, _acceptedAuthGuids);

                    sendBytes = _connectionHandler.Handle(
                        new ConnectionHandlerData
                        {
                            RecvDictionary = dictionary,
                            DiagnosticManager = _diagnosticManager,
                            ServerSocketService = _serverSocketService
                        });
                }
                catch (Exception ex)
                {
                    sendBytes = Encoding.ASCII.GetBytes("Unexpected / Unauthorized");
                    _loggerManager.RegisterLogToConsole(ex.Message);
                    _loggerManager.RegisterLogToFile(ex.Message);
                }
                finally
                {
                    socketHandler.Send(sendBytes);

                    _loggerManager.RegisterLogToConsole($"End connection from: {socketHandler.RemoteEndPoint.ToString()}");
                    _loggerManager.RegisterLogToFile($"End connection from: {socketHandler.RemoteEndPoint.ToString()}");

                    socketHandler.Shutdown(SocketShutdown.Both);
                    socketHandler.Close();
                }
            }
        }

        private interface IConnectionHandler
        {
            byte[] Handle(ConnectionHandlerData connectionHandlerData);
        }

        private struct ConnectionHandler : IConnectionHandler
        {
            public const string Web = nameof(ConnectionHandler.Web);

            public const string Diag = nameof(ConnectionHandler.Diag);

            public const string App = nameof(ConnectionHandler.App);

            public byte[] Handle(ConnectionHandlerData connectionHandlerData)
            {
                string type = connectionHandlerData.RecvDictionary["Type"];
                switch (type)
                {
                    case Web:
                        return this.WebHandler(connectionHandlerData);
                    case Diag:
                        return this.DiagnosticHandler(connectionHandlerData);
                    case App:
                        return this.AppHandler(connectionHandlerData);
                    default:
                        return Encoding.ASCII.GetBytes("Unsupported type");
                }
            }

            private byte[] DiagnosticHandler(ConnectionHandlerData connectionHandlerData)
            {
                string command = connectionHandlerData.RecvDictionary["Command"];
                string returnData = connectionHandlerData.DiagnosticManager.RunJob(command);

                return Encoding.ASCII.GetBytes(returnData);
            }

            private byte[] WebHandler(ConnectionHandlerData connectionHandlerData) =>
                connectionHandlerData.ServerSocketService.HandleWebData(connectionHandlerData.RecvDictionary);

            private byte[] AppHandler(ConnectionHandlerData connectionHandlerData) =>
                connectionHandlerData.ServerSocketService.HandleAppData(connectionHandlerData.RecvDictionary);
        }

        private struct ConnectionHandlerData
        {
            public Dictionary<string, string> RecvDictionary { get; set; }
            public IServerSocketService ServerSocketService { get; set; }
            public IDiagnosticManager DiagnosticManager { get; set; }
        }
    }
}