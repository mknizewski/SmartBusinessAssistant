using SBA.BOL.Inference.Models;
using SBA.BOL.Inference.Service;
using SBA.BOL.Web.Models;
using SBA.BOL.Web.Service;
using SBA.Core.BOL.Infrastructure;
using SBA.Core.BOL.Threads.FaqAnswerAdjusting;
using SBA.Core.BOL.Threads.HotLinksRecommender;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

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
        private readonly IWebLogService _webLogService;
        private readonly IWebSessionService _webSessionService;

        public ServerSocketManager()
        {
            _faqService = SimpleFactory.Get<FaqService, IFaqService>();
            _webLogService = SimpleFactory.Get<WebLogService, IWebLogService>();
            _webSessionService = SimpleFactory.Get<WebSessionService, IWebSessionService>();
        }

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
                case Request.Web.Logs:
                    return Logs(recvDictionary);
                case Request.Web.HotLinks:
                    return HotLinks(recvDictionary);
            }

            return null;
        }

        private byte[] HandUp(Dictionary<string, string> answerDictionary)
        {
            Task.Run(() => _faqService.AddFaqQuestion(new FaqModel.Question
            {
                AnswerId = Convert.ToInt32(answerDictionary["AnswerId"]),
                QuestionName = answerDictionary["Question"],
                InsertTime = DateTime.Now
            }));

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
                    { "Answer", _faqService.GetAnswer(possibleAnswer.AnswerId) },
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

        private byte[] Logs(Dictionary<string, string> answerDictionary)
        {
            string cookieData = answerDictionary["CookieData"];
            byte[] cookieDataByte = Convert.FromBase64String(cookieData);
            var binaryFormatter = SimpleFactory.Get<BinaryFormatter>();
            var cookieDataList = SimpleFactory.Get<List<string>>();

            using (var memoryStream = SimpleFactory.Get<MemoryStream>(cookieDataByte))
                cookieDataList = (List<string>)binaryFormatter.Deserialize(memoryStream);

            foreach (var splited in cookieDataList.Select(x => x.Split(new char[] { ';'})))
                _webLogService.AddWebLog(new WebLogModel
                {
                    SessionId = _webSessionService.AddOrGetWebSessionId(splited[0]),
                    CurrentTime = splited[1],
                    ClientIp = splited[2],
                    CurrentUrl = splited[3],
                    PreviousUrl = splited[4]
                });

            _webLogService.ProccessWebLogsToCsv(Settings.CsvPath);
            return Encoding.UTF8.GetBytes("OK");
        }

        private byte[] HotLinks(Dictionary<string ,string> answerDictionary)
        {
            string userGuid = answerDictionary["UserGuid"];
            string statTrace = answerDictionary["StatTrace"];
            string hotLinks = Settings.Supervisior.ForceRun<string>(
                nameof(HotLinksRecommenderThread),
                new string[] { userGuid, statTrace });

            return Encoding.UTF8.GetBytes(hotLinks);
        }

        private static class Request
        {
            public static class Web
            {
                public const string AnswerSuggestion = "AnswerSuggestion";
                public const string HandUp = "HandUp";
                public const string Logs = "Logs";
                public const string HotLinks = "HotLinks";
            }

            public static class App
            {

            }
        }
    }
}
