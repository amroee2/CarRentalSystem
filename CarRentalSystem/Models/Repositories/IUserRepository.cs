namespace CarRentalSystem.Models.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserAsync(string userId);
    }
}
