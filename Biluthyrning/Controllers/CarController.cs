using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biluthyrning.Models;
using Biluthyrning.Models.ViewModels.Car;
using Microsoft.AspNetCore.Mvc;

namespace Biluthyrning.Controllers
{
    public class CarController : Controller
    {

        CarService service;
        public CarController(CarService inputCarService)
        {
            service = inputCarService;
        }

        public IActionResult Index()
        {
            return View(service.GetAllCars());
        }

        [HttpGet]
        public IActionResult AddCar()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddCar(CarAddCarVM CACVM)
        {
            service.AddCar(CACVM);

            return RedirectToAction(nameof(Index));
        }
    }
}