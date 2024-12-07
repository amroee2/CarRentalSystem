namespace CarRentalSystem.Models.Repositories
{
    public interface IRentalRepository
    {
        Task<List<Rental>> GetUserRentalsAsync(string userId);
        Task<List<Rental>> GetActiveUserRentalsAsync(string userId);
    }
}
