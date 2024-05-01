using Readr.API.Models;

namespace Readr.API.DTOs
{
    public static class DtoExtensions
    {
        public static BookDto AsDto(this Book book)
        {
            return new BookDto()
            {
                Id = book.Id,
                BookTitle = new BookTitleDto()
                {
                    Id = book.BookTitle.Id,
                    Title = book.BookTitle.Title,
                    Author = book.BookTitle.Author,
                    Genre = book.BookTitle.Genre,
                    CoverUrl = book.BookTitle.Cover?.Url
                }
            };
        }
    }
}
