using System.Reflection;
using System.Text;

namespace SBA.BOL.Inference.Service
{
    public interface IServerSocketService
    {
        string HandleRecvData(byte[] recvByte);
    }

    public class ServerSocketService : IServerSocketService
    {
        // TODO: Implementacja
        public string HandleRecvData(byte[] recvByte)
        {
            string encodedData = Encoding.ASCII.GetString(recvByte).Replace("\0", string.Empty);
            return encodedData + $" - handled by {Assembly.GetCallingAssembly().FullName}";
        }
    }
}