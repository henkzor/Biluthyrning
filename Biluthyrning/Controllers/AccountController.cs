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
    public class AccountController : Controller
    {
        AccountService service;
        public AccountController(AccountService inputAccountService)
        {
            service = inputAccountService;
        }



        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return Content($"User logged in as {User.Identity.Name}");
            else
                return Content($"User not logged in");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AccountLoginVM ALVM)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Login));
            }

            var result = await service.TryLoginAsync(ALVM);
            if (!result.Succeeded)
            {
                // Show error
                ModelState.AddModelError(nameof(AccountLoginVM.FirstName), "Login failed");
                return View(ALVM);
            }

            return RedirectToAction(nameof(MemberPage));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(AccountCreateVM accountCreateInput)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Create));

            }

            var result = await service.TryRegisterAsync(accountCreateInput);
            if (!result.Succeeded)
            {
                // Show error
                ModelState.AddModelError(string.Empty, result.Errors.First().Description);
                return View(accountCreateInput);
            }
            TempData["Success"] = true;
            TempData["Message"] = "Account created succesfully!";
            return RedirectToAction(nameof(Login));
        }

        public IActionResult MemberPage()
        {
            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await service.TryLogOutAsync();
            return RedirectToAction(nameof(Login));
        }



    }
}