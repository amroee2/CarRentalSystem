namespace CarRentalSystem.Models.Repositories
{
    public interface IRentalRepository
    {
        List<Rental> GetAll(string userId);
    }
}
