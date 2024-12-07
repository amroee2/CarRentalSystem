namespace CarRentalSystem.Models.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> GetOrCreateCartAsync(string userId);
        Task AddToCartAsync(string userId, int carId, DateTime rentalStart, DateTime rentalEnd);
        Task CheckoutAsync(string userId);
        Task EmptyCartAsync(string userId);
        Task RemoveFromCartAsync(string userId, int carId);
        Task<List<Cart>> GetAllProcessedCartsAsync(string userId);
    }
}
