using SBA.BOL.Common.Factory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SBA.BOL.Inference.Service
{
    public interface IServerSocketService
    {
        Dictionary<string, string> DeserializeDictionary(byte[] recvBytes);
        byte[] HandleWebData(Dictionary<string, string> recvDictionary);
        byte[] HandleAppData(Dictionary<string, string> recvDictionary);
        void AuthorizeConnection(Dictionary<string, string> recvDictionary, string[] authGuids);
    }

    public class ServerSocketService : IServerSocketService
    {
        public void AuthorizeConnection(Dictionary<string, string> recvDictionary, string[] authGuids)
        {
            string recvGuid = recvDictionary["AuthGuid"];
            if (!authGuids.Contains(recvGuid))
                throw new UnauthorizedAccessException();
        }

        public Dictionary<string, string> DeserializeDictionary(byte[] recvBytes)
        {
            var binaryFormatter = SimpleFactory.Get<BinaryFormatter>();
            using (var memoryStream = SimpleFactory.Get<MemoryStream>(recvBytes))
                return (Dictionary<string, string>) binaryFormatter.Deserialize(memoryStream);
        }

        /// <summary>
        /// TODO: Obsłużyć.
        /// </summary>
        /// <param name="recvDictionary"></param>
        public byte[] HandleAppData(Dictionary<string, string> recvDictionary)
        {
            return Encoding.ASCII.GetBytes("test z app");
        }

        /// <summary>
        /// TODO: Obsłużyć.
        /// </summary>
        /// <param name="recvDictionary"></param>
        /// <returns></returns>
        public byte[] HandleWebData(Dictionary<string, string> recvDictionary)
        {
            return Encoding.ASCII.GetBytes("test z core");
        }
    }
}