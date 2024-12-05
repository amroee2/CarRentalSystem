using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Models.Repositories
{
    public class CarRepository: ICarRepository
    {

        private readonly CarRentalSystemDbContext _context;

        public CarRepository(CarRentalSystemDbContext context)
        {
            _context = context;
        }

        public async Task <List<Car>> GetAllCars()
        {
            return await _context.Cars.ToListAsync();
        }
    }
}
