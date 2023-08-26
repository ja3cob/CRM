using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers;

public class AdminController : Controller
{
    public AdminController() 
    {
    }

    [HttpGet]
    [Route("/uzytkownicy")]
    public IActionResult People()
    {
        return View("People");
    }
}
