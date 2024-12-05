using Microsoft.AspNetCore.Mvc;
using Storage.Application.Helpers;

namespace Equipment_Storage_Service.Controllers;

[ApiController]
[Route("/api/v1")]
public class AuthController : ControllerBase
{
    private readonly IHttpContextAccessor _contextAccessor;

    public AuthController(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
    
    [HttpGet("/auth")]
    public IActionResult Login()
    {
        var key = ApiKeyHelper.GetUniqueKey();
        
        _contextAccessor.HttpContext.Response.Cookies.Append("token", key, new CookieOptions
        {
            MaxAge = TimeSpan.FromMinutes(20),
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None
        });

        return Ok(key);
    }
}