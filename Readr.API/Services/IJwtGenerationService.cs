using Readr.API.Models;

namespace Readr.API.Services
{
    public interface IJwtGenerationService
    {
        string GenerateToken(User user);
    }
}