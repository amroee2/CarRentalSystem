using CarRentalSystem.Models;
using CarRentalSystem.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly UserManager<User> _userManager;

        public CartController(ICartRepository cartRepository, UserManager<User> userManager)
        {
            _cartRepository = cartRepository;
            _userManager = userManager;
        }

        public IActionResult Checkout()
        {
            var userId = _userManager.GetUserId(User);
            var cart = _cartRepository.GetOrCreateCart(userId);
            return View(cart);
        }

        [HttpPost]
        public IActionResult EmptyCart(string userId)
        {
            _cartRepository.EmptyCart(userId);
            return RedirectToAction("Checkout");
        }

        [HttpPost]
        public IActionResult AddToCart(string userId, int carId,DateTime start, DateTime end)
        {
            try
            {
                _cartRepository.AddToCart(userId, carId, start, end);
                return RedirectToAction("Checkout");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult CheckoutCart(string userId)
        {
            _cartRepository.Checkout(userId);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(string userId, int carId)
        {
            _cartRepository.RemoveFromCart(userId, carId);
            return RedirectToAction("Checkout");
        }

        public IActionResult Summary(string userId)
        {
            var carts = _cartRepository.GetAllProcessedCarts(userId);
            return View(carts);
        }
    }
}
