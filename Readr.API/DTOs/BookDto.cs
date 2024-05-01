namespace Readr.API.DTOs
{
    public record BookDto
    {
        public int Id { get; set; }

        public BookTitleDto BookTitle { get; set; }

    }
}
