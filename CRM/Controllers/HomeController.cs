using CRM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CRM.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
            }
            if(month > 12 || month < 1)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(new Month(DateOnly.FromDateTime(DateTime.Now), new Day[1]));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("/error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}