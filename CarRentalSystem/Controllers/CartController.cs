using CarRentalSystem.Models;
using CarRentalSystem.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly UserManager<User> _userManager;

        public CartController(ICartRepository cartRepository, UserManager<User> userManager)
        {
            _cartRepository = cartRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Checkout(string userId)
        {
            var cart = await _cartRepository.GetOrCreateCartAsync(userId);
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> EmptyCart(string userId)
        {
            await _cartRepository.EmptyCartAsync(userId);
            return RedirectToAction("Checkout", "Cart", new { userId = userId });
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(string userId, int carId,DateTime start, DateTime end)
        {
            try
            {
                await _cartRepository.AddToCartAsync(userId, carId, start, end);
                return RedirectToAction("Checkout", "Cart", new { userId = userId });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CheckoutCart(string userId)
        {
            await _cartRepository.CheckoutAsync(userId);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(string userId, int carId)
        {
            await _cartRepository.RemoveFromCartAsync(userId, carId);
            return RedirectToAction("Checkout", "Cart", new { userId = userId });
        }

        public async Task<IActionResult> Summary(string userId)
        {
            var carts = await _cartRepository.GetAllProcessedCartsAsync(userId);
            return View(carts);
        }
    }
}
