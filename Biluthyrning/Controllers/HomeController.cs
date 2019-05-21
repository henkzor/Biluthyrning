using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biluthyrning.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biluthyrning.Controllers
{
    public class HomeController : Controller
    {
        HomeService service;
        public HomeController(HomeService inputHomeService)
        {
            service = inputHomeService;
        }

        public IActionResult Index()
        {
            return View(service.CreateNewPerson());
        }

     
    }
}