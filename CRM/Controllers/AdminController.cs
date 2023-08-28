using CRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers;

public class AdminController : Controller
{
    [Authorize(Roles = nameof(Role.Admin))]
    [HttpGet]
    [Route("[action]")]
    public IActionResult People()
    {
        return View("People");
    }
}
