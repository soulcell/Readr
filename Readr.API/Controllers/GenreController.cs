using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Readr.API.Data;
using Readr.API.Models;
using Readr.API.Services;

namespace Readr.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly ReadrDbContext context;
        private readonly IUserService userService;

        public GenreController(ReadrDbContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> Get(int pageNumber, int pageSize)
        {
            return Ok(await this.context.Genres.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync());
        }

        [Route("prefered")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPreferedGenres([FromBody] IList<int> genreIds)
        {
            User? user = await this.userService.GetUserAsync(User);

            if (user is null)
            {
                return Unauthorized();
            }

            await context.Entry(user).Collection(u => u.PreferedGeneres).LoadAsync();

            await context.Genres
                .Where(genre => genreIds.Contains(genre.Id))
                .ForEachAsync(genre => user.PreferedGeneres.Add(genre));

            context.Users.Update(user);

            await context.SaveChangesAsync();

            return Ok();
        }

        [Route("prefered")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IList<Genre>>> GetPreferedGenres()
        {
            User? user = await this.userService.GetUserAsync(User);

            if (user is null)
            {
                return Unauthorized();
            }

            return await context.Entry(user).Collection(u => u.PreferedGeneres).Query().ToListAsync();

        }

        [Route("prefered")]
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> RemovePreferedGenres([FromBody] IList<int> genreIds)
        {
            User? user = await this.userService.GetUserAsync(User);

            if (user is null)
            {
                return Unauthorized();
            }

            await context.Entry(user).Collection(u => u.PreferedGeneres).LoadAsync();

            await context.Genres
                .Where(genre => genreIds.Contains(genre.Id))
                .ForEachAsync(genre => user.PreferedGeneres.Remove(genre));

            context.Users.Update(user);

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
