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

    [HttpPost]
    [Route("{username}")]
    public IActionResult EditPerson(string username, Person person)
    {
        _peopleService.Update(username, person);
        return Ok();
    }
}
