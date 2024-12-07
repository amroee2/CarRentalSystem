using CarRentalSystem.Models;
using CarRentalSystem.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace CarRentalSystem.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
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
                TempData["ErrorMessage"] = "Problem with creating a car";
                return RedirectToAction("Index", "Home");
            }
            car.IsAvailable = Request.Form["IsAvailable"] == "true";
            await _carRepository.AddCar(car);
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
