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
    public ActionResult<Person> GetPerson(int id)
    {
        var person = _peopleService.Get(id);
        if(person == null)
        {
            return NotFound();
    }
        return Ok(person);
    }
    [HttpPost]
    [Route("{id}")]
    public IActionResult EditPerson(int id, Person person)
    {
        _peopleService.Update(id, person);
        return Ok();
    }
}
