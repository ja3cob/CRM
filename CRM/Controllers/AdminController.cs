using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    public class AdminController : Controller
    {
        public AdminController() 
        {
        }

        [Route("/uzytkownicy")]
        [HttpGet]
        public IActionResult People()
        {
            return View("People");
        }
    }
}
