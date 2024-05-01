using Readr.API.Models;

namespace Readr.API.DTOs
{
    public record BookTitleDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public Genre Genre { get; set; } 

        public string? CoverUrl { get; set; }
    }
}
