namespace CarRentalSystem.Models.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUser(string userId);
    }
}
