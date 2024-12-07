namespace CarRentalSystem.Models.Repositories
{
    public interface ICarRepository
    {

        Task<List<Car>> GetAllCarsAsync();
        Task<Car> GetCarAsync(int id);
        Task AddCarAsync(Car car);
        Task DeleteCarAsync(int id);
        Task UpdateCarAsync(Car car);
        List<Car> SearchCarsAsync(string searchText);
    }
}
