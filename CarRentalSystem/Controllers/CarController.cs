using CarRentalSystem.Models;
using CarRentalSystem.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Car car)
        {
            if (!ModelState.IsValid)
            {
                return View(car);
            }
            car.IsAvailable = Request.Form["IsAvailable"] == "true";
            await _carRepository.AddCar(car);
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Update(Car car)
        {
            await _carRepository.UpdateCar(car);
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _carRepository.DeleteCar(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
