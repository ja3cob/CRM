using CRM.Models.Database;
using CRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers.Api
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoItemsController : Controller
    {
        [HttpGet]
        public ActionResult<IEnumerable<ToDoItem>> GetForMonth([FromQuery] int? year, [FromQuery] int? month)
        {
            if (!year.HasValue || !month.HasValue)
            {
                year = DateTime.Now.Year;
                month = DateTime.Now.Month;
            }
            if(month > 12 || month < 1)
            {
                return NotFound();
            }
            return Ok(CalendarService.GetToDoItems(year.Value, month.Value));
        }
    }
}
