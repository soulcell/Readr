using System.ComponentModel.DataAnnotations.Schema;

namespace Readr.API.Models
{
    public class BookTitle : BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public Genre Genre { get; set; }
        public BookCover? Cover { get; set; }

        public BookTitle(int id, string title, string author, Genre genre, BookCover? cover = null) : base(id)
        {
            Title = title;
            Author = author;
            Genre = genre;
            Cover = cover;
        }

        private BookTitle() { }
    }
}
