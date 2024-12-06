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

        public List<Rental> GetUserRentals(string userId)
        {
            return _context.Rentals
                .Where(r => r.UserId == userId)
                .Include(r => r.Car)
                .ToList();
        }

        public List<Rental> GetActiveUserRentals(string userId)
        {
            return _context.Rentals
                .Where(r => r.UserId == userId && r.RentalEndDate > DateTime.Now)
                .Include(r => r.Car)
                .ToList();
        }
    }
}
