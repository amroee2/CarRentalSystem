using CarRentalSystem.Models;
using CarRentalSystem.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController: Controller
    {

        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public AdminController(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _userRepository.GetUsersAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(string userId)
        {
            var user = await _userRepository.GetUser(userId);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TerminateSession(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID cannot be null or empty.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.UpdateSecurityStampAsync(user);
            }

            return RedirectToAction("Users");
        }

    }
}
