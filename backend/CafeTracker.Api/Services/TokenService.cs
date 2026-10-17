using System.Security.Claims;
using CafeTracker.Data.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace CafeTracker.Api.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly RsaSecurityKey _signingKey;

    public TokenService(IConfiguration config, RsaSecurityKey signingKey)
    {
        _config = config;
        _signingKey = signingKey;
    }

    public string CreateToken(User user)
    {
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
            }),
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"],
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.RsaSha256),
        };

        return new JsonWebTokenHandler().CreateToken(descriptor);
    }
}
