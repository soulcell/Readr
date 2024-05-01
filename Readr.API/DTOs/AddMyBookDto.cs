namespace Readr.API.DTOs
{
    public record AddMyBookDto
    {
        public int? BookTitleId { get; set; }

        public string BookTitle { get; set; }

        public string BookAuthor { get; set; }

        public int GenreId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
