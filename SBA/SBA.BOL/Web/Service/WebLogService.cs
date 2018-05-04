using SBA.BOL.Common.Factory;
using SBA.BOL.Web.Models;
using SBA.DAL.Context.InferenceDb.Entity;
using SBA.DAL.Context.InferenceDb.Repository.WebLog;

namespace SBA.BOL.Web.Service
{
    public interface IWebLogService
    {
        void AddWebLog(WebLogModel webLog);
    }

    public class WebLogService : IWebLogService
    {
        private readonly IWebLogRepository _webLogRepository;

        public WebLogService() => 
            _webLogRepository = SimpleFactory.Get<WebLogRepository, IWebLogRepository>();

        public void AddWebLog(WebLogModel webLog) =>
            _webLogRepository.SaveWebLog(new WebLog
            {
                SessionId = webLog.SessionId,
                ClientIp = webLog.ClientIp,
                CurrentTime = webLog.CurrentTime,
                CurrentUrl = webLog.CurrentUrl,
                PreviousUrl = webLog.PreviousUrl
            });
    }
}