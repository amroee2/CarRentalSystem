using CarRentalSystem.Models;
using CarRentalSystem.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Controllers
{
    [Authorize]
    public class RentalController : Controller
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalController(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public IActionResult UserRentals(string userId)
        {
            var rentals = _rentalRepository.GetUserRentals(userId);
            return View(rentals);
        }
    }
}
