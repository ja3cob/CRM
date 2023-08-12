﻿using CRM.Models;
using CRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers.Api
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoItemsController : Controller
    {
        private readonly ToDoItemsService _toDoItemsService;
        private readonly PeopleService _peopleService;
        public ToDoItemsController(ToDoItemsService toDoItemsService, PeopleService peopleService)
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

        [HttpPost]
        public ActionResult Save([FromBody] ToDoItem item)
        {
            try
            {
                _toDoItemsService.Save(item);
            }
            catch(Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
