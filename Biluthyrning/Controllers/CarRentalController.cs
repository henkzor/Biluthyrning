using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biluthyrning.Models;
using Biluthyrning.Models.Data;
using Biluthyrning.Models.ViewModels;
using Biluthyrning.Models.ViewModels.CarRental;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biluthyrning.Controllers
{
    [Authorize]
    public class CarRentalController : Controller
    {
        CarRentalService service;
        public CarRentalController(CarRentalService inputCarRentalService)
        {
            service = inputCarRentalService;
        }
        public IActionResult Index()
        {
            return View(service.GetAllBookings());
        }

        [HttpGet]
        public IActionResult Booking()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Booking(CarRentalBookingVM CRBVM)
        {
            int bookingNr = service.CreateBooking(CRBVM);

            return RedirectToAction(nameof(ShowBooking), new { id = bookingNr });
        }

        public IActionResult Return()
        {
            return View();
        }

        public IActionResult GetBookingInfo([FromBody]InputNrDM INDM)
        {
            return Json(service.GetBookingInfo(INDM.Id));
        }

        [HttpGet]
        public IActionResult ShowBooking(int id)
        {
            return View(service.GetCarForShowBooking(id));
        }

        [HttpPost]
        public IActionResult Return(CarRentalReturnVM CRRVM)
        {
            service.ReturnCar(CRRVM);

            return RedirectToAction(nameof(ShowBooking), new { id = CRRVM.BookingNr });
        }

        [HttpPost]
        public IActionResult CheckAvailability([FromBody]CarRentalCheckAvailabilityVM data)
        {
            return Json(service.CheckCarAvailability(data));
        }

    }
}