using Readr.API.Models;

namespace Readr.API.Services
{
    public interface IBookCoverService
    {
        Task<string?> FindBookCoverUrlAsync(BookTitle bookTitle);
    }
}
