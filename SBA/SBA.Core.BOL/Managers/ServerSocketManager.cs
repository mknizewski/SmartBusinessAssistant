using SBA.BOL.Inference.Models;
using SBA.Core.BOL.Infrastructure;
using SBA.Core.BOL.Threads.FaqAnswerAdjusting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SBA.Core.BOL.Managers
{
    public interface IServerSocketManager
    {
        Dictionary<string, string> DeserializeDictionary(byte[] recvBytes);
        byte[] HandleWebData(Dictionary<string, string> recvDictionary);
        byte[] HandleAppData(Dictionary<string, string> recvDictionary);
        void AuthorizeConnection(Dictionary<string, string> recvDictionary, string[] authGuids);
    }

    public class ServerSocketManager : IServerSocketManager
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
                return (Dictionary<string, string>)binaryFormatter.Deserialize(memoryStream);
        }

        /// <summary>
        /// TODO: Obsłużyć.
        /// </summary>
        /// <param name="recvDictionary"></param>
        public byte[] HandleAppData(Dictionary<string, string> recvDictionary)
        {
            return Encoding.ASCII.GetBytes("test z app");
        }

        public byte[] HandleWebData(Dictionary<string, string> recvDictionary)
        {
            string request = recvDictionary[nameof(Request)];
            switch (request)
            {
                case Request.Web.AnswerSuggestion:
                    return Encoding.UTF8.GetBytes(AnswerSuggestion(recvDictionary["Question"]));
            }

            return null;
        }

        private string AnswerSuggestion(string userQuestion)
        {
            var decides = Settings.Supervisior.ForceRun<List<FaqModel.Decide>>(
                nameof(FaqAnswerAdjustingThread),
                new string[] { userQuestion });

            string possibleAnswer = decides
                .Where(x => x.DecideStatus)
                .OrderByDescending(x => x.Propability)
                .Select(x => x.Answer)
                .FirstOrDefault();

            return string.IsNullOrEmpty(possibleAnswer) ?
                "Przykro nam, w danej chwili nie znaleźliśmy odpowiedzi na Twoje pytanie. Prosimy poczekać na odpowiedź poprzez e-mail." :
                possibleAnswer;
        }

        private static class Request
        {
            public static class Web
            {
                public const string AnswerSuggestion = "AnswerSuggestion";
            }

            public static class App
            {

            }
        }
    }
}
