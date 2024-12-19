using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Storage.Application.Helpers.Jwt;

namespace Equipment_Storage_Service.Controllers;

[ApiController]
[Route("/api/v1/")]
public class AuthController : ControllerBase
{
    private readonly IOptionsMonitor<JwtOptions> _jwtOptions;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IOptionsMonitor<JwtOptions> jwtOptions,  ILogger<AuthController> logger)
    {
        _jwtOptions = jwtOptions;
        _logger = logger;
    }
    
    [HttpPost("login")]
    public IActionResult Login()
    {
        var token = GenerateJwtToken();
        
        return Ok(token);
    }

    private string GenerateJwtToken()
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.CurrentValue.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.CurrentValue.Issuer,
            audience: _jwtOptions.CurrentValue.Audience,
            claims: new List<Claim>(),
            expires: DateTime.UtcNow.Add(_jwtOptions.CurrentValue.Expiration),
            signingCredentials: credentials);
        
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        
        _logger.LogInformation("Jwt token successfully generated");
        
        return jwt;
    }
}