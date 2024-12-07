using CarRentalSystem.Models;
using CarRentalSystem.Models.Repositories;
using Microsoft.EntityFrameworkCore;

public class CartRepository : ICartRepository
{
    private readonly CarRentalSystemDbContext _context;
    private readonly IRentalRepository _rentalRepository;
    public CartRepository(CarRentalSystemDbContext context, IRentalRepository rentalRepository)
    {
        _context = context;
        _rentalRepository = rentalRepository;
    }

    public async Task<Cart> GetOrCreateCartAsync(string userId)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Car)
            .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsCheckedOut);

        if (cart == null)
        {
            cart = new Cart
            {
                UserId = userId,
                IsCheckedOut = false,
                TotalCost = 0
            };
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        return cart;
    }

    public async Task AddToCartAsync(string userId, int carId, DateTime rentalStart, DateTime rentalEnd)
    {
        var cart = await GetOrCreateCartAsync(userId);
        if (cart.CartItems.Any(ci => ci.CarId == carId))
        {
            ModifyExistingCarDateAsync(cart, carId, rentalStart, rentalEnd);
            return;
        }
        var car = await _context.Cars.FindAsync(carId);

        try
        {
            await ValidateAdditionAsync(cart, car, rentalEnd, rentalStart, userId);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }

        var rentalDays = (rentalEnd - rentalStart).Days;

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
        await _context.SaveChangesAsync();
    }

    public async Task CheckoutAsync(string userId)
    {
        var cart = await GetOrCreateCartAsync(userId);

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

            await _context.Rentals.AddAsync(rental);

            var car = await _context.Cars.FindAsync(item.CarId);
            if (car != null) car.IsAvailable = false;
        }
        cart.CheckedOutDate = DateTime.Now;
        cart.IsCheckedOut = true;
        await _context.SaveChangesAsync();
    }

    public async Task EmptyCartAsync(string userId)
    {
        var cart = await GetOrCreateCartAsync(userId);
        cart.CartItems.Clear();
        cart.TotalCost = 0;
       await _context.SaveChangesAsync();
    }

    public async Task RemoveFromCartAsync(string userId, int carId)
    {
        var cart = await GetOrCreateCartAsync(userId);
        var cartItem = cart.CartItems.FirstOrDefault(ci => ci.CarId == carId);
        if (cartItem == null) return;

        cart.TotalCost -= cartItem.Cost;
        cart.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Cart>> GetAllProcessedCartsAsync(string userId)
    {
        return await _context.Carts
            .Where(c => c.IsCheckedOut)
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Car)
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }

    public async Task ModifyExistingCarDateAsync(Cart cart, int carId, DateTime rentalStart, DateTime rentalEnd)
    {
        var existingCartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.CarId == carId);
        cart.TotalCost -= existingCartItem.Cost;
        existingCartItem.RentalStart = rentalStart;
        existingCartItem.RentalEnd = rentalEnd;
        existingCartItem.Cost = (rentalEnd - rentalStart).Days * existingCartItem.Car.RentPricePerDay;
        cart.TotalCost += existingCartItem.Cost;
       await _context.SaveChangesAsync();
    }

    public async Task ValidateAdditionAsync(Cart cart, Car car, DateTime rentalEnd, DateTime rentalStart, string userId)
    {
        if (car == null || !car.IsAvailable)
            throw new InvalidOperationException("Car not available.");

        var rentalDays = (rentalEnd - rentalStart).Days;
        if (rentalDays <= 0 || rentalStart < DateTime.Now || rentalStart> rentalEnd)
        {
            throw new InvalidOperationException("Invalid rental period.");
        }

        foreach (var item in cart.CartItems)
        {
            if ((rentalStart >= item.RentalStart && rentalStart <= item.RentalEnd) ||
                (rentalEnd >= item.RentalStart && rentalEnd <= item.RentalEnd) ||
                (rentalStart <= item.RentalStart && rentalEnd >= item.RentalEnd))
            {
                throw new InvalidOperationException("Car already rented for this period.");
            }
        }

        var rentedCars = await _rentalRepository.GetActiveUserRentalsAsync(userId);

        foreach (var rental in rentedCars)
        {
            if ((rentalStart >= rental.RentalStartDate && rentalStart <= rental.RentalEndDate) ||
                (rentalEnd >= rental.RentalStartDate && rentalEnd <= rental.RentalEndDate) ||
                (rentalStart <= rental.RentalStartDate && rentalEnd >= rental.RentalEndDate))
            {
                throw new InvalidOperationException("A Car already rented for this period.");
            }
        }
    }
}
