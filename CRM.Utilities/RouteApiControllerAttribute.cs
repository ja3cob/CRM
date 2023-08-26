using Microsoft.AspNetCore.Mvc;

namespace CRM.Utilities;

public class RouteApiControllerAttribute : RouteAttribute
{
    public RouteApiControllerAttribute(string? template = null) : base("api/[controller]" + template)
    {
    }
}
