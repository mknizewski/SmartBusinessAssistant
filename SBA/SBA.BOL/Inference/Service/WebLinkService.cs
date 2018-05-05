using SBA.BOL.Common.Factory;
using SBA.BOL.Inference.Models;
using SBA.DAL.Context.InferenceDb.Entity;
using SBA.DAL.Context.InferenceDb.Repository.WebLink;
using System.Collections.Generic;
using System.Linq;

namespace SBA.BOL.Inference.Service
{
    public interface IWebLinkService
    {
        List<WebLinkModel> GetWebLinks();
        int AddOrGetWebLink(WebLinkModel webLinkModel);
    }

    public class WebLinkService : IWebLinkService
    {
        private readonly IWebLinkRepository _webLinkRepository;

        public WebLinkService() =>
            _webLinkRepository = SimpleFactory.Get<WebLinkRepository, IWebLinkRepository>();

        public int AddOrGetWebLink(WebLinkModel webLinkModel) =>
            _webLinkRepository.AddOrGetWebLink(new WebLinks
            {
                Url = webLinkModel.Url
            });

        public List<WebLinkModel> GetWebLinks() =>
            _webLinkRepository
                .GetWebLinks()
                .Select(x => new WebLinkModel
                {
                    Id = x.Id,
                    Url = x.Url
                }).ToList();
    }               
}