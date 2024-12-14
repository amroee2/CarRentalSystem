using CarRentalSystem.Models;
using CarRentalSystem.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        public async Task<IActionResult> Details(int id)
        {
            return View(await _carRepository.GetCarAsync(id));
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
                TempData["ErrorMessage"] = "Problem with creating a car";
                return RedirectToAction("Index", "Home");
            }
            car.IsAvailable = Request.Form["IsAvailable"] == "true";
            await _carRepository.AddCarAsync(car);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Car car)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Problem with updating a car";
                return RedirectToAction("Index", "Home");
            }
            car.IsAvailable = Request.Form["IsAvailable"] == "true";
            await _carRepository.UpdateCarAsync(car);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _carRepository.DeleteCarAsync(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
