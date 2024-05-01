namespace Readr.API.Models
{
    public class BookCover : BaseEntity
    {
        public string Url { get; set; }

        public BookCover(int id, string url) : base(id)
        {
            Url = url;
        }

        private BookCover() { }
    }
}
