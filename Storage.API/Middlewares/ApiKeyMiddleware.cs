using System.Net;
using Microsoft.VisualBasic;

namespace Equipment_Storage_Service.Middlewares;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiKeyMiddleware> _logger;

    public ApiKeyMiddleware(RequestDelegate next, ILogger<ApiKeyMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/auth"))
        {
            await _next(context);
            return;
        }
        
        var token = context.Request.Cookies["token"];
        
        if (string.IsNullOrEmpty(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            
            await context.Response.WriteAsync("Token is missing in cookies.");
            
            _logger.LogInformation("Token is missing in cookies.");
            return;
        }

        // Proceed to the next middleware
        await _next(context);
    }
}