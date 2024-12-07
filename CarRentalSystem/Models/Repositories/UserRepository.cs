using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Models.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly CarRentalSystemDbContext _context;

        public UserRepository(CarRentalSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUser(string userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
