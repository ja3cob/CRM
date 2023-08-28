using CRM.Database;
using CRM.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CRM.Services;

public class AuthenticationService
{
    private readonly CRMDbContext _dbContext;
    public AuthenticationService(CRMDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> Login(string username, string password, HttpContext context)
    {
        if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            throw new ApiException("Username and password must be specified.", System.Net.HttpStatusCode.BadRequest);
        }

        var person = _dbContext.People.FirstOrDefault(x => x.Username == username);
        if (person == null)
        {
            return false;
        }
        if(BCrypt.Net.BCrypt.Verify(password, person.Password))
        {
            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, person.Username),
               new Claim(ClaimTypes.Role, person.Role.ToString())
            };
            var identity = new ClaimsIdentity(claims, Cookies.Identity);
            var principal = new ClaimsPrincipal(identity);
            await context.SignInAsync(principal);
            return true;
        }
        return false;
    }
}
