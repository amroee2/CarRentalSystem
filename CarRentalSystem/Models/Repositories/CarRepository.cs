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

        public async Task <List<Car>> GetAllCarsAsync()
        {
            return await _context.Cars.AsNoTracking().ToListAsync();
        }

        public async Task<Car> GetCarAsync(int id)
        {
            return await _context.Cars.AsNoTracking().FirstOrDefaultAsync(c => c.CarId == id);
        }

        public async Task AddCarAsync(Car car)
        {
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCarAsync(Car car)
        {
            _context.Update(car);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.CarId == id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        public List<Car> SearchCarsAsync(string searchText)
        {
            return _context.Cars.AsNoTracking().Where(c => c.CarName.Contains(searchText) || c.CarModel.Contains(searchText)).ToList();
        }
    }
}
