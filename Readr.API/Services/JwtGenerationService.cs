using Readr.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Readr.API.Services
{
    public class JwtGenerationService : IJwtGenerationService
    {
        private readonly IConfiguration configuration;

        public JwtGenerationService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber!),
            };

            if (user.UserName is not null)
            { 
                claims.Add(new Claim(ClaimTypes.Name, user.UserName)); 
            }

            var jwt = new JwtSecurityToken(
                configuration["JWT:Issuer"],
                configuration["JWT:Audience"],
                claims,
                null,
                DateTime.UtcNow.AddDays(2d),
                credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
