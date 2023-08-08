using CRM.Models.Database;
using CRM.Services;
using CRM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers.Api
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoItemsController : Controller
    {
        private readonly IToDoItemsService _toDoItemsService;
        private readonly PeopleService _peopleService;
        public ToDoItemsController(IToDoItemsService toDoItemsService, PeopleService peopleService)
        {
            _toDoItemsService = toDoItemsService;
            _peopleService = peopleService;
        }

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
            return Ok(_toDoItemsService.GetList(year.Value, month.Value));
        }
    }
}
