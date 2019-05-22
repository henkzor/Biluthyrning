using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biluthyrning.Models;
using Biluthyrning.Models.ViewModels.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biluthyrning.Controllers
{
    [Authorize]
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

        [HttpPost]
        public IActionResult CleanCar([FromBody]CarPerformActionVM data)
        {
            service.CleanCar(data);
            return Json(null); // Gör detta utan att redirecta?
        }

        public IActionResult ServiceCar([FromBody]CarPerformActionVM data)
        {
            service.ServiceCar(data);
            return Json(null); // Gör detta utan att redirecta?
        }

        public IActionResult RemoveCar([FromBody]CarPerformActionVM data)
        {
            service.RemoveCar(data);
            return Json(null); // Gör detta utan att redirecta?
        }
    }
}