using SBA.BOL.Common.Factory;
using SBA.BOL.Inference.Models;
using SBA.BOL.Inference.Service;
using SBA.BOL.Web.Models;
using SBA.DAL.Context.InferenceDb.Entity;
using SBA.DAL.Context.InferenceDb.Repository.WebLog;
using System.Linq;

namespace SBA.BOL.Web.Service
{
    public interface IWebLogService
    {
        void AddWebLog(WebLogModel webLog);
        void ProccessWebLogsToCsv(string csvPath);
        int GetLastInsertedId();
    }

    public class WebLogService : IWebLogService
    {
        private readonly IWebLinkService _webLinkService;
        private readonly IWebLogRepository _webLogRepository;
        private readonly ICsvLogDataFileService _csvLogDataFileService;

        public WebLogService()
        {
            _webLinkService = SimpleFactory.Get<WebLinkService, IWebLinkService>();
            _webLogRepository = SimpleFactory.Get<WebLogRepository, IWebLogRepository>();
            _csvLogDataFileService = SimpleFactory.Get<CsvLogDataFileService, ICsvLogDataFileService>();
        }

        public void AddWebLog(WebLogModel webLog) =>
            _webLogRepository.SaveWebLog(new WebLog
            {
                SessionId = webLog.SessionId,
                ClientIp = webLog.ClientIp,
                CurrentTime = webLog.CurrentTime,
                CurrentUrl = webLog.CurrentUrl,
                PreviousUrl = webLog.PreviousUrl
            });

        public void ProccessWebLogsToCsv(string csvPath)
        {
            var csvFile = _csvLogDataFileService.GetCsv(csvPath);
            var webLogs = _webLogRepository
                .GetWebLogs()
                .GroupBy(x => x.SessionId);

            foreach (var sessonId in webLogs)
            {
                foreach (var log in sessonId)
                {
                    int linkId = _webLinkService.AddOrGetWebLink(new WebLinkModel { Url = log.CurrentUrl });
                    var csvRow = csvFile.Rows
                        .FirstOrDefault(x => x.SessionId == log.SessionId &&
                                             x.UrlId == linkId);

                    if (csvRow != null)
                        ProcessLogWhenCsvRowIsExists(csvRow);
                    else
                        ProcessLogWhenCsvRowIsNotExists(
                            csvFile,
                            log.SessionId,
                            linkId);

                    _webLogRepository.SetEntityProcessed(log.Id);
                }
            }

            AddNotVisitedRowsToExistedLogs(csvFile);
            _csvLogDataFileService.SaveCsv(csvFile, csvPath);
        }

        private void ProcessLogWhenCsvRowIsExists(CsvDataFile.CsvDataFileRow csvDataFileRow) =>
            csvDataFileRow.VisitCount += 1.0;

        private void ProcessLogWhenCsvRowIsNotExists(
            CsvDataFile csvDataFile,
            int sessionId,
            int linkId) => 
            csvDataFile.Rows.Add(new CsvDataFile.CsvDataFileRow
            {
                SessionId = sessionId,
                UrlId = linkId,
                VisitCount = 0.0
            });

        private void AddNotVisitedRowsToExistedLogs(CsvDataFile csvDataFile)
        {
            var webLinksIds = _webLinkService
                .GetWebLinks()
                .Select(x => x.Id);

            var csvRowsGroupedBySessionId = csvDataFile
                .Rows
                .GroupBy(x => x.SessionId);

            foreach (var row in csvRowsGroupedBySessionId)
            {
                var visitedLinks = row.Select(x => x.UrlId);
                foreach (int linkId in webLinksIds)
                {
                    if (!visitedLinks.Contains(linkId))
                        csvDataFile.Rows.Add(new CsvDataFile.CsvDataFileRow
                        {
                            SessionId = row.Key,
                            UrlId = linkId,
                            VisitCount = 0.0
                        });
                }
            }
        }

        public int GetLastInsertedId() =>
            _webLogRepository.GetLastInsertedId();
    }
}