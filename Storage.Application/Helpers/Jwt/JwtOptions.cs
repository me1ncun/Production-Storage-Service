namespace Storage.Application.Helpers.Jwt;

public class JwtOptions
{
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public string SecretKey { get; set; }
    public TimeSpan Expiration { get; set; }
}