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

        [HttpGet]
        public IActionResult Details(int id)
        {

            return View(service.GetCarByID(id));
        }


        [HttpPost]
        public IActionResult AddCar(CarAddCarVM CACVM)
        {
            service.AddCar(CACVM);
            TempData["Success"] = true;
            TempData["Message"] = "Car added!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult CleanCar([FromBody]CarPerformActionVM data)
        {
            service.CleanCar(data);
            return Json(null); 
        }

        public IActionResult ServiceCar([FromBody]CarPerformActionVM data)
        {
            service.ServiceCar(data);
            return Json(null);
        }

        public IActionResult RemoveCar([FromBody]CarPerformActionVM data)
        {
            service.RemoveCar(data);
            return Json(null); 
        }
    }
}