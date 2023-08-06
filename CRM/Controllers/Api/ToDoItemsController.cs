﻿using CRM.Models.Database;
using CRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers.Api
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoItemsController : Controller
    {
        private IToDoItemsService _toDoItemsService;
        public ToDoItemsController(IToDoItemsService toDoItemsService)
        {
            _toDoItemsService = toDoItemsService;
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
            return Ok(_toDoItemsService.GetToDoItems(year.Value, month.Value));
        }
    }
}
