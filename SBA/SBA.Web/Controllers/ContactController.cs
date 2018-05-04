using SBA.BOL.Web.Models;
using SBA.BOL.Web.Service;
using System.Threading.Tasks;
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
        public virtual async Task<JsonResult> Send(ContactModel contactModel)
        {
            _contactService.AddContact(contactModel);
            return Json(
                await _clientSocketService.SendUserQuestionToGetSuggestAnswer(contactModel.Message),
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual async Task<JsonResult> HandUp(QuestionModel questionModel) =>
            Json(await _clientSocketService.SendHandUp(questionModel));
    }
}