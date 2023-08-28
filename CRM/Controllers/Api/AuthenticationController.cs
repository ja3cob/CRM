using CRM.Models.Api;
using CRM.Services;
using CRM.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers.Api;

[ApiController]
[RouteApiController]
public class AuthenticationController : Controller
{
    private readonly AuthService _service;
    public AuthenticationController(AuthService service)
    {
        _service = service;
    }
    [AllowAnonymous]
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!await _service.Login(request.Username, request.Password, HttpContext))
        {
            return Unauthorized();
        }
        return Ok();
    }
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Logout()
    {
        await _service.Logout(HttpContext);
        return Ok();
    }
}
