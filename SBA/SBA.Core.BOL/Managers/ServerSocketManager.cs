using SBA.BOL.Inference.Models;
using SBA.BOL.Inference.Service;
using SBA.BOL.Web.Models;
using SBA.BOL.Web.Service;
using SBA.Core.BOL.Infrastructure;
using SBA.Core.BOL.Threads.FaqAnswerAdjusting;
using SBA.Core.BOL.Threads.GoogleCse;
using SBA.Core.BOL.Threads.HotLinksRecommender;
using SBA.Core.BOL.Threads.StructureExtractor;
using SBA.DAL.Context.InferenceDb.Entity;
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
        private readonly ICseStructuresService _cseStructuresService;

        public ServerSocketManager()
        {
            _faqService = SimpleFactory.Get<FaqService, IFaqService>();
            _webLogService = SimpleFactory.Get<WebLogService, IWebLogService>();
            _webSessionService = SimpleFactory.Get<WebSessionService, IWebSessionService>();
            _cseStructuresService = SimpleFactory.Get<CseStructuresService, ICseStructuresService>();
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

        public byte[] HandleAppData(Dictionary<string, string> recvDictionary)
        {
            string request = recvDictionary[nameof(Request)];
            switch (request)
            {
                case Request.App.FaqDataAnswers:
                    return FaqDataAnswers(recvDictionary);
                case Request.App.FaqDataQuestions:
                    return FaqDataQuestions(recvDictionary);
                case Request.App.FaqDeleteAnswer:
                    return FaqDeleteAnswer(recvDictionary);
                case Request.App.FaqDeleteQuestion:
                    return FaqDeleteQuestion(recvDictionary);
                case Request.App.FaqEditAnswer:
                    return FaqEditAnswer(recvDictionary);
                case Request.App.FaqEditQuestion:
                    return FaqEditQuestion(recvDictionary);
                case Request.App.FaqAddQuestion:
                    return FaqAddQuestion(recvDictionary);
                case Request.App.Logs:
                    return SendLogsToApp(recvDictionary);
                case Request.App.RecommendData:
                    return RecommendData(recvDictionary);
                case Request.App.DataDetails:
                    return DataDetails(recvDictionary);
                case Request.App.ToFavorites:
                    return ToFavorites(recvDictionary);
                case Request.App.GetFavorites:
                    return GetFavorites(recvDictionary);
                case Request.App.RecommendOnDemand:
                    return RecommendOnDemand(recvDictionary);
            }

            return null;
        }

        private byte[] RecommendOnDemand(Dictionary<string, string> recvDictionary)
        {
            string keywords = recvDictionary["Keywords"];
            Settings.Supervisior.ForceRun<Nothing>(nameof(GoogleCseThread), keywords);
            Settings.Supervisior.ForceRun<Nothing>(nameof(StructureExtractorThread));

            return Encoding.UTF8.GetBytes("OK");
        }

        private byte[] GetFavorites(Dictionary<string, string> recvDictionary)
        {
            var favoritesDataDictionary = SimpleFactory.Get<List<Dictionary<string, string>>>();
            var favoritesArticles = _cseStructuresService
                .GetFavoritesData<ArticleCse>()
                .Select(x => new Dictionary<string, string>
                {
                    { "Id", x.Id.ToString() },
                    { "Type", nameof(ArticleCse) },
                    { "Title", x.Title },
                    { "InsertTime", x.InsertTime.ToString("yyyy-MM-dd HH:mm") }
                }).ToList();

            var favoritesEvents = _cseStructuresService
                .GetFavoritesData<EventCse>()
                .Select(x => new Dictionary<string, string>
                {
                    { "Id", x.Id.ToString() },
                    { "Type", nameof(EventCse) },
                    { "Title", x.Title },
                    { "InsertTime", x.InsertTime.ToString("yyyy-MM-dd HH:mm") }
                }).ToList();

            var favoritesOrganizations = _cseStructuresService
                .GetFavoritesData<OrganizationCse>()
                .Select(x => new Dictionary<string, string>
                {
                    { "Id", x.Id.ToString() },
                    { "Type", nameof(OrganizationCse) },
                    { "Title", x.Title },
                    { "InsertTime", x.InsertTime.ToString("yyyy-MM-dd HH:mm") }
                }).ToList();

            var favoritesPersons = _cseStructuresService
                .GetFavoritesData<PersonCse>()
                .Select(x => new Dictionary<string, string>
                {
                    { "Id", x.Id.ToString() },
                    { "Type", nameof(PersonCse) },
                    { "Title", x.Title },
                    { "InsertTime", x.InsertTime.ToString("yyyy-MM-dd HH:mm") }
                }).ToList();

            var favoritesVideos = _cseStructuresService
                .GetFavoritesData<VideoCse>()
                .Select(x => new Dictionary<string, string>
                {
                    { "Id", x.Id.ToString() },
                    { "Type", nameof(VideoCse) },
                    { "Title", x.Title },
                    { "InsertTime", x.InsertTime.ToString("yyyy-MM-dd HH:mm") }
                }).ToList();

            favoritesDataDictionary.AddRange(favoritesArticles);
            favoritesDataDictionary.AddRange(favoritesEvents);
            favoritesDataDictionary.AddRange(favoritesOrganizations);
            favoritesDataDictionary.AddRange(favoritesPersons);
            favoritesDataDictionary.AddRange(favoritesVideos);

            var binaryFormatter = SimpleFactory.Get<BinaryFormatter>();
            using (var memoryStream = SimpleFactory.Get<MemoryStream>())
            {
                binaryFormatter.Serialize(memoryStream, favoritesDataDictionary);
                return memoryStream.ToArray();
            }
        }

        private byte[] ToFavorites(Dictionary<string, string> recvDictionary)
        {
            int id = Convert.ToInt32(recvDictionary["Id"]);
            string type = recvDictionary["DataType"];
            bool returnType;

            if (type == "Article")
                returnType = _cseStructuresService.SetToFavorites<ArticleCse>(id);
            else if (type == "Event")
                returnType = _cseStructuresService.SetToFavorites<EventCse>(id);
            else if (type == "Organization")
                returnType = _cseStructuresService.SetToFavorites<OrganizationCse>(id);
            else if (type == "Person")
                returnType = _cseStructuresService.SetToFavorites<PersonCse>(id);
            else
                returnType = _cseStructuresService.SetToFavorites<VideoCse>(id);

            var binaryFormatter = SimpleFactory.Get<BinaryFormatter>();
            using (var memoryStream = SimpleFactory.Get<MemoryStream>())
            {
                binaryFormatter.Serialize(memoryStream, returnType);
                return memoryStream.ToArray();
            }
        }

        private byte[] DataDetails(Dictionary<string, string> recvDictionary)
        {
            int id = Convert.ToInt32(recvDictionary["Id"]);
            string type = recvDictionary["DataType"];
            var returnData = SimpleFactory.Get<Dictionary<string, string>>();

            if (type == "Article")
            {
                var articleData = _cseStructuresService.GetData<ArticleCse>(id);
                articleData
                    .GetType()
                    .GetProperties()
                    .ToList()
                    .ForEach(x => returnData.Add(x.Name, x.GetValue(articleData, null)?.ToString()));
                
            }
            else if (type == "Event")
            {
                var eventData = _cseStructuresService.GetData<EventCse>(id);
                eventData
                    .GetType()
                    .GetProperties()
                    .ToList()
                    .ForEach(x => returnData.Add(x.Name, x.GetValue(eventData, null)?.ToString()));
            }
            else if (type == "Organization")
            {
                var organizationData = _cseStructuresService.GetData<OrganizationCse>(id);
                organizationData
                    .GetType()
                    .GetProperties()
                    .ToList()
                    .ForEach(x => returnData.Add(x.Name, x.GetValue(organizationData, null)?.ToString()));
            }
            else if (type == "Person")
            {
                var personData = _cseStructuresService.GetData<PersonCse>(id);
                personData
                    .GetType()
                    .GetProperties()
                    .ToList()
                    .ForEach(x => returnData.Add(x.Name, x.GetValue(personData, null)?.ToString()));
            }
            else
            {
                var videoData = _cseStructuresService.GetData<VideoCse>(id);
                videoData
                    .GetType()
                    .GetProperties()
                    .ToList()
                    .ForEach(x => returnData.Add(x.Name, x.GetValue(videoData, null)?.ToString()));
            }

            var binaryFormatter = SimpleFactory.Get<BinaryFormatter>();
            using (var memoryStream = SimpleFactory.Get<MemoryStream>())
            {
                binaryFormatter.Serialize(memoryStream, returnData);
                return memoryStream.ToArray();
            }
        }

        private byte[] RecommendData(Dictionary<string, string> recvDictionary)
        {
            var notShowedDataDictionary = SimpleFactory.Get<List<Dictionary<string, string>>>();
            var notShowedArticles = _cseStructuresService
                .GetNotShowedData<ArticleCse>()
                .Select(x => new Dictionary<string, string>
                {
                    { "Id", x.Id.ToString() },
                    { "Type", nameof(ArticleCse) },
                    { "Title", x.Title },
                    { "InsertTime", x.InsertTime.ToString("yyyy-MM-dd HH:mm") }
                }).ToList();

            var notShowedEvents = _cseStructuresService
                .GetNotShowedData<EventCse>()
                .Select(x => new Dictionary<string, string>
                {
                    { "Id", x.Id.ToString() },
                    { "Type", nameof(EventCse) },
                    { "Title", x.Title },
                    { "InsertTime", x.InsertTime.ToString("yyyy-MM-dd HH:mm") }
                }).ToList();

            var notShowedOrganizations = _cseStructuresService
                .GetNotShowedData<OrganizationCse>()
                .Select(x => new Dictionary<string, string>
                {
                    { "Id", x.Id.ToString() },
                    { "Type", nameof(OrganizationCse) },
                    { "Title", x.Title },
                    { "InsertTime", x.InsertTime.ToString("yyyy-MM-dd HH:mm") }
                }).ToList();

            var notShowedPersons = _cseStructuresService
                .GetNotShowedData<PersonCse>()
                .Select(x => new Dictionary<string, string>
                {
                    { "Id", x.Id.ToString() },
                    { "Type", nameof(PersonCse) },
                    { "Title", x.Title },
                    { "InsertTime", x.InsertTime.ToString("yyyy-MM-dd HH:mm") }
                }).ToList();

            var notShowedVideos = _cseStructuresService
                .GetNotShowedData<VideoCse>()
                .Select(x => new Dictionary<string, string>
                {
                    { "Id", x.Id.ToString() },
                    { "Type", nameof(VideoCse) },
                    { "Title", x.Title },
                    { "InsertTime", x.InsertTime.ToString("yyyy-MM-dd HH:mm") }
                }).ToList();

            notShowedDataDictionary.AddRange(notShowedArticles);
            notShowedDataDictionary.AddRange(notShowedEvents);
            notShowedDataDictionary.AddRange(notShowedOrganizations);
            notShowedDataDictionary.AddRange(notShowedPersons);
            notShowedDataDictionary.AddRange(notShowedVideos);

            var binaryFormatter = SimpleFactory.Get<BinaryFormatter>();
            using (var memoryStream = SimpleFactory.Get<MemoryStream>())
            {
                binaryFormatter.Serialize(memoryStream, notShowedDataDictionary);
                return memoryStream.ToArray();
            }
        }

        private byte[] SendLogsToApp(Dictionary<string, string> recvDictionary)
        {
            var binaryFormatter = SimpleFactory.Get<BinaryFormatter>();
            var logs = _webLogService
                .GetWebLogs()
                .Select(x => new Dictionary<string, string>
                {
                    { "Id", x.Id.ToString() },
                    { "SessionId", x.SessionId.ToString() },
                    { "CurrentTime", x.CurrentTime },
                    { "ClientIp", x.ClientIp },
                    { "CurrentUrl", x.CurrentUrl },
                    { "PreviousUrl", x.PreviousUrl }
                }).ToList();

            using (var memoryStream = SimpleFactory.Get<MemoryStream>())
            {
                binaryFormatter.Serialize(memoryStream, logs);
                return memoryStream.ToArray();
            }
        }

        private byte[] FaqAddQuestion(Dictionary<string, string> recvDictionary)
        {
            _faqService.AddFaqQuestion(new FaqModel.Question
            {
                AnswerId = Convert.ToInt32(recvDictionary["AnswerId"]),
                QuestionName = recvDictionary["Question"],
                InsertTime = DateTime.Now
            });

            return Encoding.UTF8.GetBytes("OK");
        }

        private byte[] FaqEditQuestion(Dictionary<string, string> recvDictionary)
        {
            _faqService.EditFaqQuestion(
                Convert.ToInt32(recvDictionary["Id"]),
                Convert.ToInt32(recvDictionary["AnswerId"]),
                recvDictionary["Question"]);

            return Encoding.UTF8.GetBytes("OK");
        }

        private byte[] FaqEditAnswer(Dictionary<string, string> recvDictionary)
        {
            _faqService.EditFaqAnswer(
                Convert.ToInt32(recvDictionary["Id"]),
                recvDictionary["Answer"]);

            return Encoding.UTF8.GetBytes("OK");
        }

        private byte[] FaqDeleteQuestion(Dictionary<string, string> recvDictionary)
        {
            _faqService.DeleteFaqQuestion(Convert.ToInt32(recvDictionary["Id"]));
            return Encoding.UTF8.GetBytes("OK");
        }

        private byte[] FaqDeleteAnswer(Dictionary<string, string> recvDictionary)
        {
            _faqService.DeleteFaqAnswer(Convert.ToInt32(recvDictionary["Id"]));
            return Encoding.UTF8.GetBytes("OK");
        }

        private byte[] FaqDataAnswers(Dictionary<string, string> recvDictionary)
        {
            var binaryFormatter = SimpleFactory.Get<BinaryFormatter>();
            var faqAnswers = _faqService
                .GetFaqAnswers()
                .Select(x => new Dictionary<string, string>
                {
                    { "Id", x.Id.ToString() },
                    { "AnswerName", x.AnswerName }
                }).ToList();

            using (var memoryStream = SimpleFactory.Get<MemoryStream>())
            {
                binaryFormatter.Serialize(memoryStream, faqAnswers);
                return memoryStream.ToArray();
            }
        }

        public byte[] FaqDataQuestions(Dictionary<string, string> dictionary)
        {
            var binaryFormatter = SimpleFactory.Get<BinaryFormatter>();
            var faqQuestions = _faqService
                .GetFaqQuestions()
                .Select(x => new Dictionary<string, string>
                {
                    { "Id", x.Id.ToString() },
                    { "AnswerId", x.AnswerId.ToString() },
                    { "QuestionName", x.QuestionName },
                    { "InsertTime", x.InsertTime.ToString("yyyy-MM-dd HH:mm") }
                }).ToList();

            using (var memoryStream = SimpleFactory.Get<MemoryStream>())
            {
                binaryFormatter.Serialize(memoryStream, faqQuestions);
                return memoryStream.ToArray();
            }
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
                public const string FaqDataAnswers = "FaqDataAnswers";
                public const string FaqDataQuestions = "FaqDataQuestions";
                public const string FaqDeleteAnswer = "FaqDeleteAnswer";
                public const string FaqDeleteQuestion = "FaqDeleteQuestion";
                public const string FaqEditAnswer = "FaqEditAnswer";
                public const string FaqEditQuestion = "FaqEditQuestion";
                public const string FaqAddQuestion = "FaqAddQuestion";
                public const string Logs = "Logs";
                public const string RecommendData = "RecommendData";
                public const string DataDetails = "DataDetails";
                public const string ToFavorites = "ToFavorites";
                public const string GetFavorites = "GetFavorites";
                public const string RecommendOnDemand = "RecommendOnDemand";
            }
        }
    }
}