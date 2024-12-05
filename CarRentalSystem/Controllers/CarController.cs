using CarRentalSystem.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly ILogger<HomeController> _logger;

        public CarController(ICarRepository carRepository, ILogger<HomeController> logger)
        {
            _carRepository = carRepository;
            _logger = logger;
        }
        public async Task<IActionResult> Details(int id)
        {
            return View(await _carRepository.GetCar(id));
        }
    }
}
