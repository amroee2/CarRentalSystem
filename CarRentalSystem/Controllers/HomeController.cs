using CarRentalSystem.Models;
using CarRentalSystem.Models.Repositories;
using CarRentalSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarRentalSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ICarRepository carRepository)
        {
            _logger = logger;
            _carRepository = carRepository;
        }

        public async Task<IActionResult> Index()
        {
            CarListViewModel carListViewModel = new CarListViewModel(await _carRepository.GetAllCarsAsync());
            return View(carListViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
