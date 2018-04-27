using SBA.BOL.Web.Models;
using SBA.BOL.Web.Service;
using System.Web.Mvc;

namespace SBA.Web.Controllers
{
    public partial class ContactController : BaseController
    {
        private readonly IContactService _contactService;
        private readonly IClientSocketService _clientSocketService;

        public ContactController(
            IContactService contactService,
            IClientSocketService clientSocketService)
        {
            _contactService = contactService;
            _clientSocketService = clientSocketService;
        }

        public virtual ActionResult Index() =>
            View();

        [HttpPost]
        public virtual JsonResult Send(ContactModel contactModel)
        {
            string possibleAnswer = _clientSocketService.SendUserQuestionToGetSuggestAnswer(contactModel.Message);

            _contactService.AddContact(contactModel);
            return Json(possibleAnswer);
        }
    }
}