using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biluthyrning.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biluthyrning.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        EventService service;
        public EventController(EventService inputEventService)
        {
            service = inputEventService;
        }

        public IActionResult Index()
        {
            return View(service.GetAllEvents());
        }


    }
}