namespace SBA.BOL.Inference.Service
{
    public interface IServerSocketService
    {
        void HandleRecvData(byte[] recvByte);
    }

    public class ServerSocketService : IServerSocketService
    {
        // TODO: Implementacja
        public void HandleRecvData(byte[] recvByte)
        {
            
        }
    }
}