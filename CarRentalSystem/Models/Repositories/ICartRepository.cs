namespace CarRentalSystem.Models.Repositories
{
    public interface ICartRepository
    {
        Cart GetOrCreateCart(string userId);
        void AddToCart(string userId, int carId, DateTime rentalStart, DateTime rentalEnd);
        void Checkout(string userId);
        void EmptyCart(string userId);
        void RemoveFromCart(string userId, int carId);
    }
}
