using SBA.BOL.Web.Models;
using SBA.BOL.Web.Service;
using System.Web.Mvc;

namespace SBA.Web.Controllers
{
    public partial class ContactController : BaseController
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService) =>
            _contactService = contactService;

        public virtual ActionResult Index() =>
            View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Send(ContactModel contactModel)
        {
            _contactService.AddContact(contactModel);
            SetAlert(Infrastructure.Alert.SystemAlert.Type.Success, "Dziękujemy za zgłoszenie!");
            return RedirectToAction(MVC.Contact.Index());
        }
    }
}