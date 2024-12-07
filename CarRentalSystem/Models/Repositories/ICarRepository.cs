namespace CarRentalSystem.Models.Repositories
{
    public interface ICarRepository
    {

        Task<List<Car>> GetAllCars();
        Task<Car> GetCar(int id);
        Task AddCar(Car car);
        Task DeleteCar(int id);
        Task UpdateCar(Car car);
        List<Car> SearchCars(string searchText);
    }
}
