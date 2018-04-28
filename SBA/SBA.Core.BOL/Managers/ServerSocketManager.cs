using SBA.BOL.Inference.Models;
using SBA.BOL.Inference.Service;
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
        private readonly IFaqService _faqService;

        public ServerSocketManager() => 
            _faqService = SimpleFactory.Get<FaqService, IFaqService>();

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
                    return AnswerSuggestion(recvDictionary["Question"]);
                case Request.Web.HandUp:
                    return HandUp(recvDictionary);
            }

            return null;
        }

        private byte[] HandUp(Dictionary<string, string> answerDictionary)
        {
            _faqService.AddFaqQuestion(new FaqModel.Question
            {
                AnswerId = Convert.ToInt32(answerDictionary["AnswerId"]),
                QuestionName = answerDictionary["Question"],
                InsertTime = DateTime.Now
            });
            return Encoding.UTF8.GetBytes("Dziękujemy za opinię.");
        }

        private byte[] AnswerSuggestion(string userQuestion)
        {
            var decides = Settings.Supervisior.ForceRun<List<FaqModel.Decide>>(
                nameof(FaqAnswerAdjustingThread),
                new string[] { userQuestion });

            var possibleAnswer = decides
                .Where(x => x.DecideStatus)
                .OrderByDescending(x => x.Propability)
                .FirstOrDefault();

            var responseDictionary = possibleAnswer != null ?
                new Dictionary<string, dynamic>
                {
                    { "HaveAnswer", true },
                    { "Answer", possibleAnswer.Answer },
                    { "AnswerId", possibleAnswer.AnswerId },
                    { "Propability", (int) (possibleAnswer.Propability * 100) },
                    { "Question", possibleAnswer.Question }
                } :
                new Dictionary<string, dynamic>
                {
                    { "HaveAnswer", false },
                    { "ErrorMessage", @"Przykro nam, w danej chwili nie znaleźliśmy odpowiedzi na Twoje pytanie. 
                                        Prosimy poczekać na odpowiedź poprzez e-mail." }
                };

            var binaryFormatter = SimpleFactory.Get<BinaryFormatter>();
            using (var memoryStream = SimpleFactory.Get<MemoryStream>())
            {
                binaryFormatter.Serialize(memoryStream, responseDictionary);
                return memoryStream.ToArray();
            }
        }

        private static class Request
        {
            public static class Web
            {
                public const string AnswerSuggestion = "AnswerSuggestion";
                public const string HandUp = "HandUp";
            }

            public static class App
            {

            }
        }
    }
}
