using SBA.BOL.Web.Models;
using SBA.DAL.Context.WebDb.Entity;
using SBA.DAL.Context.WebDb.Repository.Contacts;
using System.Collections.Generic;
using System.Linq;

namespace SBA.BOL.Web.Service
{
    public interface IContactService
    {
        void AddContact(ContactModel contactModel);
        List<ContactModel> GetContacts();
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

        public List<ContactModel> GetContacts() =>
            _contactRepository.GetContacts()
                .Select(x => new ContactModel
                {
                    Id = x.Id.ToString(),
                    Subject = x.Subject,
                    Email = x.Email,
                    Message = x.Message,
                    MobileNumber = x.Message,
                    Name = x.Name
                }).ToList();
    }
}
