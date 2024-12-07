using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Models.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync();
    }
}
