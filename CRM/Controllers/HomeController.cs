using CRM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CRM.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }

        [HttpGet]
        public IActionResult ToDoItems()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("/error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}