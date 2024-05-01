using Microsoft.EntityFrameworkCore;
using Readr.API.Data;
using Readr.API.Models;
using System.Security.Claims;

namespace Readr.API.Services
{
    public class UserService : IUserService
    {
        private readonly ReadrDbContext context;

        public UserService(ReadrDbContext dbContext)
        {
            this.context = dbContext;
        }

        public async Task CreateAsync(User user)
        {
            // TODO: Add unique phone number check
            await this.context.Users.AddAsync(user);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            this.context.Users.Update(user);
            await this.context.SaveChangesAsync();
        }

        public async Task RemoveAsync(User user)
        {
            this.context.Users.Remove(user);
            await this.context.SaveChangesAsync();
        }

        public async Task<User?> FindAsync(int id)
        {
            return await this.context.Users.FindAsync(id);
        }

        public async Task<User?> FindByPhoneNumberAsync(string phoneNumber)
        {
            return await this.context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        }

        public async Task<User?> GetUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            int id;
            if (int.TryParse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier), out id))
            {
                return await this.context.Users.FindAsync(id);
            }

            string? phoneNumber = claimsPrincipal.FindFirstValue(ClaimTypes.MobilePhone);
            if (phoneNumber != null)
            {
                return await FindByPhoneNumberAsync(phoneNumber);
            }

            return await Task.FromResult<User?>(null);
        }
    }
}
