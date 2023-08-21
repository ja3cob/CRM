using CRM.Models;
using CRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers.Api;

[ApiController]
[Route("[controller]")]
public class PeopleController : Controller
{
    private readonly PeopleService _peopleService;
	public PeopleController(PeopleService peopleService)
    {
        _peopleService = peopleService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Person>> GetAllPeople()
    {
        return Ok(_peopleService.GetList());
    }

    [HttpPost]
    public IActionResult AddPerson(Person person)
    {
        _peopleService.Add(person);
        return Ok();
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetPerson(int id)
    {
        return Ok(_peopleService.Get(id));
    }
    [HttpPost]
    [Route("{id}")]
    public IActionResult EditPerson(int id, Person person)
    {
        _peopleService.Update(id, person);
        return Ok();
    }
}
