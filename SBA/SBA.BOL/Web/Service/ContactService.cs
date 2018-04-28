using SBA.BOL.Web.Models;
using SBA.DAL.Context.WebDb.Entity;
using SBA.DAL.Context.WebDb.Repository.Contacts;

namespace SBA.BOL.Web.Service
{
    public interface IContactService
    {
        void AddContact(ContactModel contactModel);
    }

    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository) =>
            _contactRepository = contactRepository;

        public void AddContact(ContactModel contactModel) =>
            _contactRepository.AddContact(new Contact
            {
                Name = contactModel.Name,
                Subject = contactModel.Subject,
                Email = contactModel.Email,
                Message = contactModel.Message,
                MobilePhone = contactModel.MobileNumber
            });
    }
}
