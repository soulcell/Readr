using Readr.API.Models;
using System.Security.Claims;

namespace Readr.API.Services
{
    public interface IUserService
    {
        Task CreateAsync(User user);
        Task<User?> FindAsync(int id);
        Task<User?> FindByPhoneNumberAsync(string phoneNumber);
        Task<User?> GetUserAsync(ClaimsPrincipal claimsPrincipal);
        Task RemoveAsync(User user);
        Task UpdateAsync(User user);
    }
}