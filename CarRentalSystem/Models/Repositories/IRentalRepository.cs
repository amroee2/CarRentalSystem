namespace CarRentalSystem.Models.Repositories
{
    public interface IRentalRepository
    {
        List<Rental> GetUserRentals(string userId);
        List<Rental> GetActiveUserRentals(string userId);
    }
}
