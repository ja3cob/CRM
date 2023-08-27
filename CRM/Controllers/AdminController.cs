using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers;

public class AdminController : Controller
{

    [HttpGet]
    [Route("[action]")]
    public IActionResult People()
    {
        return View("People");
    }
}
