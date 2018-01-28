using System.ComponentModel.DataAnnotations;

namespace SBA.DAL.Context.WebDb.Entity
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
