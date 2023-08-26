using CRM.Models;
using CRM.Services;
using CRM.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers.Api;

[ApiController]
[RouteApiController]
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
    public ActionResult<int> AddPerson([FromBody] Person person)
    {
        var id = _peopleService.Add(person);
        return Ok(id);
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
    public IActionResult EditPerson(int id, [FromBody] Person person)
    {
        _peopleService.Update(id, person);
        return Ok();
    }
    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeletePerson(int id)
    {
        _peopleService.Delete(id);
        return Ok();
    }
}
