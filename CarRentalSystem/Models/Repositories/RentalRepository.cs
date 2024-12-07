using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Models.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly CarRentalSystemDbContext _context;
        public RentalRepository(CarRentalSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<Rental>> GetUserRentalsAsync(string userId)
        {
            return await _context.Rentals
                .Where(r => r.UserId == userId)
                .Include(r => r.Car)
                .ToListAsync();
        }

        public async Task<List<Rental>> GetActiveUserRentalsAsync(string userId)
        {
            return await _context.Rentals
                .Where(r => r.UserId == userId && r.RentalEndDate > DateTime.Now)
                .Include(r => r.Car)
                .ToListAsync();
        }
    }
}
