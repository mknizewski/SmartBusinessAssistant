using SBA.DAL.Context.WebDb.Entity;
using System.Collections.Generic;
using System.Linq;

namespace SBA.DAL.Context.WebDb.Repository.Contacts
{
    public interface IContactRepository : IBaseRepository
    {
        IEnumerable<Contact> GetContacts();
        void AddContact(Contact contact);
    }

    public class ContactRepository : BaseRepository, IContactRepository
    {
        public void AddContact(Contact contact)
        {
            Add(contact);
            SaveChanges();
        }

        public IEnumerable<Contact> GetContacts() =>
            Queryable<Contact>()
                .ToList();
    }
}
