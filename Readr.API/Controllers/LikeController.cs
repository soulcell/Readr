using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Readr.API.Data;
using Readr.API.Models;
using Readr.API.Services;

namespace Readr.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ReadrDbContext dbContext;
        private readonly IUserService userService;

        public LikeController(ReadrDbContext dbContext, IUserService userService)
        {
            this.dbContext = dbContext;
            this.userService = userService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Like(int bookId, BookLikeStatus likeStatus)
        {
            User? user = await this.userService.GetUserAsync(User);

            if (user is null)
            {
                return Unauthorized();
            }

            Book? book = await this.dbContext.Books.FindAsync(bookId);

            if (book is null)
            {
                return NotFound();
            }

            BookLike bookLike = new BookLike(0, user, book, likeStatus);

            await this.dbContext.BookLikes.AddAsync(bookLike);
            await this.dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
