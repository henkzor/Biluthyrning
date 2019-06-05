using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biluthyrning.Models;
using Biluthyrning.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biluthyrning.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        CustomerService service;
        public CustomerController(CustomerService inputCustomerService)
        {
            service = inputCustomerService;
        }

        public IActionResult Index()
        {
            return View(service.GetAllCustomers());
        }

        public IActionResult Details(int id)
        {
            return View(service.GetCustomerByID(id));
        }
    }
}