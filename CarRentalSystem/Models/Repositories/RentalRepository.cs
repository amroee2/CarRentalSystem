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

        public List<Rental> GetAll(string userId)
        {
            return _context.Rentals
                .Where(r => r.UserId == userId)
                .Include(r => r.Car)
                .ToList();
        }
    }
}
