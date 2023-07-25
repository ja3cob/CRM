using CRM.Services;
using CRM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CRM.Controllers
{
    public class CalendarController : Controller
    {
        public CalendarController() { }

        [HttpGet]
        public IActionResult Calendar([FromRoute] int? year, [FromRoute] int? month)
        {
            if(!year.HasValue || !month.HasValue)
            {
                year = DateTime.Now.Year;
                month = DateTime.Now.Month;
            }
            if(month > 12 || month < 1)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(CalendarService.GenerateMonth(month.Value, year.Value));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}