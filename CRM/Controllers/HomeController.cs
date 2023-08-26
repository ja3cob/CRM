using CRM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CRM.Controllers;

    public class HomeController : Controller
    {
        public HomeController() { }

        [HttpGet]
        public IActionResult ToDoItems()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/account/login")]
        public IActionResult Login()
            if(User.Identity?.IsAuthenticated ?? false)
        {
                return Redirect("/");
            }
            return View("/Views/Account/Login.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [HttpGet]
        [Route("/error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }