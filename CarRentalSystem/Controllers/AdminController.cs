using CarRentalSystem.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Controllers
{
    public class AdminController: Controller
    {

        private readonly IUserRepository _userRepository;

        public AdminController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _userRepository.GetUsersAsync();
            return View(users);
        }
    }
}
