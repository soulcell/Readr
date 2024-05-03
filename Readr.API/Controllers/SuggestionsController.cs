using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Readr.API.Data;
using Readr.API.DTOs;
using Readr.API.Models;
using Readr.API.Services;

namespace Readr.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SuggestionsController : ControllerBase
    {
        private readonly ReadrDbContext dbContext;
        private readonly IUserService userService;

        public SuggestionsController(ReadrDbContext dbContext, IUserService userService)
        {
            this.dbContext = dbContext;
            this.userService = userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<BookDto>>> Get()
        {
            User? user = await this.userService.GetUserAsync(User);

            if (user is null)
            {
                return Unauthorized();
            }

            var minTime = DateTime.UtcNow.AddHours(-1);

            var booksLiked = this.dbContext.BookLikes
                .Include(bl => bl.User)
                .Include(bl => bl.Book)
                .Where(bl => bl.User == user)
                .Where(bl => bl.CreatedAt >= minTime)
                .Select(bl => bl.Book);

            var books = this.dbContext.Books
                .Include(b => b.BookTitle)
                .Include(b => b.BookTitle.Genre)
                .Include(b => b.BookTitle.Cover)
                .Include(b => b.Owner)
                .Where(b => b.Owner != user)
                .Where(b => !booksLiked.Contains(b));

            //await dbContext.Entry(user).Collection(u => u.PreferedGeneres).LoadAsync();

            if (user.PreferedGeneres.Count > 0)
            {
                //books = books.Where(b => user.PreferedGeneres.Contains(b.BookTitle.Genre));
            }

            return Ok(books.Select(b => b.AsDto()));
        }
    }
}
