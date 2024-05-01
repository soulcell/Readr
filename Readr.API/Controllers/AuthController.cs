using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Readr.API.DTOs;
using Readr.API.Models;
using Readr.API.Services;

namespace Readr.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly ISmsService smsService;
        private readonly IUserService userService;
        private readonly IJwtGenerationService jwtGenerationService;

        public AuthController(IAuthService authService, ISmsService smsService, IUserService userService, IJwtGenerationService jwtGenerationService)
        {
            this.authService = authService;
            this.smsService = smsService;
            this.userService = userService;
            this.jwtGenerationService = jwtGenerationService;
        }

        /// <summary>
        /// Sends security code by SMS
        /// </summary>
        /// <param name="dto"></param>
        /// <remarks>
        /// Sample request: 
        /// 
        ///     POST /getcode
        ///     {
        ///         "Phone": "123456"
        ///     }
        ///     
        /// </remarks>
        [Route("getcode")]
        [HttpPost]
        public async Task<IActionResult> GetSecurityCode([FromBody] GetSecurityCodeDto dto)
        {
            string code = authService.GetSecurityCode(dto.Phone);
            bool smsResult = await smsService.Send(dto.Phone, code);

            return smsResult ? Ok() : StatusCode(500);
        }

        [Route("checkcode")]
        [HttpPost]
        public async Task<IActionResult> CheckSecuityCode([FromBody] CheckSecurityCodeDto dto)
        {
            bool result = authService.CheckSecurityCode(dto.Phone, dto.Code);

            if (!result)
            {
                return Unauthorized();
            }

            User? user = await userService.FindByPhoneNumberAsync(dto.Phone);

            if (user is null)
            {
                user = new User(dto.Phone);
                await userService.CreateAsync(user);
            }

            string token = jwtGenerationService.GenerateToken(user);

            return Ok(token);
        }

        [Route("setname")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SetUserName([FromBody] SetUserNameDto dto)
        {
            User? user = await userService.GetUserAsync(User);
            if (user is null)
            {
                return Unauthorized();
            }

            user.UserName = dto.Name;
            await userService.UpdateAsync(user);

            return Ok();
        }

    }
}
