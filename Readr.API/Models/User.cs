using Microsoft.AspNetCore.Identity;

namespace Readr.API.Models
{
    public class User : BaseEntity
    {
        public string? UserName { get; set; }

        public string PhoneNumber { get; set; }

        public virtual IEnumerable<Book> Books { get; set; } = new List<Book>();

        public ISet<Genre> PreferedGeneres { get; set; } = new HashSet<Genre>();

        public User(string phone)
        {
            PhoneNumber = phone;
        }

        private User() { }
    }
}
