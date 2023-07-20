using CRM.Services;
using CRM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CRM.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }

        public IActionResult Index()
        {
            return View(new IndexViewModel(CalendarService.GenerateMonth(DateTime.Now.Month, DateTime.Now.Year)));
        }
        [HttpPost]
        [Route("[controller]")]
        public IActionResult Post()
        {
            return Ok("cipka");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}