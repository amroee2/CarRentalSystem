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
            return await _context.Cars.AsNoTracking().ToListAsync();
        }

        public async Task<Car> GetCar(int id)
        {
            return await _context.Cars.AsNoTracking().FirstOrDefaultAsync(c => c.CarId == id);
        }

        public async Task AddCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCar(Car car)
        {
            _context.Update(car);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCar(int id)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.CarId == id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        public List<Car> SearchCars(string searchText)
        {
            return _context.Cars.Where(c => c.CarName.Contains(searchText) || c.CarModel.Contains(searchText)).ToList();
        }
    }
}
