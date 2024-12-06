using CarRentalSystem.Models;
using CarRentalSystem.Models.Repositories;
using Microsoft.EntityFrameworkCore;

public class CartRepository : ICartRepository
{
    private readonly CarRentalSystemDbContext _context;

    public CartRepository(CarRentalSystemDbContext context)
    {
        _context = context;
    }

    public Cart GetOrCreateCart(string userId)
    {
        var cart = _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Car)
            .FirstOrDefault(c => c.UserId == userId && !c.IsCheckedOut);

        if (cart == null)
        {
            cart = new Cart
            {
                UserId = userId,
                IsCheckedOut = false,
                TotalCost = 0
            };
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        return cart;
    }

    public void AddToCart(string userId, int carId, DateTime rentalStart, DateTime rentalEnd)
    {
        var cart = GetOrCreateCart(userId);
        if (cart.CartItems.Any(ci => ci.CarId == carId))
        {
            var existingCartItem = _context.CartItems.FirstOrDefault(ci => ci.CarId == carId);
            cart.TotalCost -= existingCartItem.Cost;
            existingCartItem.RentalStart = rentalStart;
            existingCartItem.RentalEnd = rentalEnd;
            existingCartItem.Cost = (rentalEnd - rentalStart).Days * existingCartItem.Car.RentPricePerDay;
            cart.TotalCost += existingCartItem.Cost;
            _context.SaveChanges();
            return;
        }
        var car = _context.Cars.Find(carId);

        if (car == null || !car.IsAvailable)
            throw new InvalidOperationException("Car not available.");

        var rentalDays = (rentalEnd - rentalStart).Days;
        if (rentalDays <= 0) throw new ArgumentException("Invalid rental duration.");

        var rentalCost = rentalDays * car.RentPricePerDay;

        var cartItem = new CartItem
        {
            CarId = carId,
            RentalStart = rentalStart,
            RentalEnd = rentalEnd,
            Cost = rentalCost
        };

        cart.CartItems.Add(cartItem);
        cart.TotalCost += rentalCost;
        _context.SaveChanges();
    }

    public void Checkout(string userId)
    {
        var cart = GetOrCreateCart(userId);

        if (cart.CartItems.Count == 0)
            throw new InvalidOperationException("Cart is empty.");

        foreach (var item in cart.CartItems)
        {
            var rental = new Rental
            {
                UserId = userId,
                CarId = item.CarId,
                RentalStartDate = item.RentalStart,
                RentalEndDate = item.RentalEnd,
                TotalCost = (double)item.Cost
            };

            _context.Rentals.Add(rental);

            var car = _context.Cars.Find(item.CarId);
            if (car != null) car.IsAvailable = false;
        }
        cart.CheckedOutDate = DateTime.Now;
        cart.IsCheckedOut = true;
        _context.SaveChanges();
    }

    public void EmptyCart(string userId)
    {
        var cart = GetOrCreateCart(userId);
        cart.CartItems.Clear();
        cart.TotalCost = 0;
        _context.SaveChanges();
    }

    public void RemoveFromCart(string userId, int carId)
    {
        var cart = GetOrCreateCart(userId);
        var cartItem = cart.CartItems.FirstOrDefault(ci => ci.CarId == carId);
        if (cartItem == null) return;

        cart.TotalCost -= cartItem.Cost;
        cart.CartItems.Remove(cartItem);
        _context.SaveChanges();
    }

    public List<Cart> GetAllCarts(string userId)
    {
        return _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Car)
            .Where(c => c.UserId == userId)
            .ToList();
    }
}
