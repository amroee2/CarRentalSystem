using CarRentalSystem.Models;
using CarRentalSystem.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Controllers
{
    public class RentalController : Controller
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly UserManager<User> _userManager;

        public RentalController(IRentalRepository rentalRepository, UserManager<User> userManager)
        {
            _rentalRepository = rentalRepository;
            _userManager = userManager;
        }

        public IActionResult UserRentals()
        {
            var userId = _userManager.GetUserId(User);
            var rentals = _rentalRepository.GetAll(userId);
            return View(rentals);
        }
    }
}
