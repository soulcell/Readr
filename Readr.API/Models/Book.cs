using System.ComponentModel.DataAnnotations.Schema;

namespace Readr.API.Models
{
    public class Book : BaseEntity
    {
        public BookTitle BookTitle { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public User Owner { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Book(int id, BookTitle bookTitle, User owner, double latitude, double longitude) : base(id)
        {
            BookTitle = bookTitle;
            Owner = owner;
            Latitude = latitude;
            Longitude = longitude;
        }

        private Book() { }
    }
}
