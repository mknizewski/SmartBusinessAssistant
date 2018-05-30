using SBA.BOL.Web.Models;
using SBA.BOL.Web.Service;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web.Http;

namespace SBA.Web.Controllers
{
    public class ClientController : ApiController
    {
        private readonly IContactService _contactService;
        private readonly IArticleService _articleService;

        public ClientController(
            IContactService contactService,
            IArticleService articleService)
        {
            _contactService = contactService;
            _articleService = articleService;
        }

        [HttpGet]
        public IEnumerable<ContactModel> Messages(string id)
        {
            string appGuid = ConfigurationManager.AppSettings[nameof(appGuid)];

            if (id != appGuid)
                throw new HttpResponseException(HttpStatusCode.Forbidden);

            return _contactService.GetContacts();
        }

        [HttpPost]
        public IHttpActionResult SaveArticle([FromBody]Dictionary<string, string> value) => 
            _articleService.AddArticleFromJson(value) ?
                     Content(HttpStatusCode.OK, true) :
                     Content(HttpStatusCode.Unauthorized, false);
    }
}