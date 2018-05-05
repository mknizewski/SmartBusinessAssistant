using SBA.BOL.Common.Factory;
using SBA.DAL.Context.InferenceDb.Entity;
using SBA.DAL.Context.InferenceDb.Repository.WebSessions;

namespace SBA.BOL.Inference.Service
{
    public interface IWebSessionService
    {
        int AddOrGetWebSessionId(string webSessionId);
    }

    public class WebSessionService : IWebSessionService
    {
        private readonly IWebSessionRepository _webSessionRepository;

        public WebSessionService() => 
            _webSessionRepository = SimpleFactory.Get<WebSessionRepository, IWebSessionRepository>();

        public int AddOrGetWebSessionId(string webSessionId) =>
            _webSessionRepository.AddOrGetWebSessionId(new WebSessionsId
            {
                SessionId = webSessionId
            });
    }
}