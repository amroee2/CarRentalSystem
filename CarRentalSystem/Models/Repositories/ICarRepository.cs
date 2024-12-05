namespace CarRentalSystem.Models.Repositories
{
    public interface ICarRepository
    {

        Task<List<Car>> GetAllCars();
    }
}
