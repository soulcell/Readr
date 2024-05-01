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
    public class BookController : ControllerBase
    {
        private readonly ReadrDbContext dbContext;
        private readonly IUserService userService;

        public BookController(ReadrDbContext dbContext, IUserService userService)
        {
            this.dbContext = dbContext;
            this.userService = userService;
        }

        [Route("mybooks")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IList<BookDto>>> GetMyBooks()
        {
            User? user = await this.userService.GetUserAsync(User);

            if (user is null)
            {
                return Unauthorized();
            }


            if (user.Books is null)
            {
                return NotFound();
            }

            return await dbContext.Entry(user)
                .Collection(u => u.Books)
                .Query()
                .Include(b => b.BookTitle)
                .Include(b => b.BookTitle.Genre)
                .Include(b => b.BookTitle.Cover)
                .Select(b => b.AsDto())
                .ToListAsync();
        }

        [Route("mybooks")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddMyBook([FromBody] AddMyBookDto dto)
        {
            User? user = await this.userService.GetUserAsync(User);

            if (user is null)
            {
                return Unauthorized();
            }

            BookTitle? bookTitle = await dbContext.BookTitles.FindAsync(dto.BookTitleId);

            if (bookTitle is null)
            {
                BookCover? cover = null;
                Genre? genre = await dbContext.Genres.FindAsync(dto.GenreId);

                if (genre is null)
                {
                    return BadRequest($"Could not found genre with the id: {dto.GenreId}");
                }

                bookTitle = new BookTitle(0, dto.BookTitle, dto.BookAuthor, genre, cover);

                await dbContext.BookTitles.AddAsync(bookTitle);
            }            

            Book book = new Book(0, bookTitle, user, dto.Latitude, dto.Longitude);

            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        [Route("mybooks/{id}")]
        [HttpPut]
        [Authorize]
        public IActionResult UpdateMyBook([FromBody] Book dto, int id)
        {
            throw new NotImplementedException();
        }

        [Route("mybooks/{id}")]
        [HttpDelete]
        [Authorize]
        public IActionResult RemoveMyBook([FromBody]Book dto, int id)
        {
            throw new NotImplementedException();
        }
    }
}
