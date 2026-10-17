using System.Security.Claims;
using CafeTracker.Api.DTOs.Auth;
using CafeTracker.Api.Services;
using CafeTracker.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CafeTracker.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _users;
    private readonly ITokenService _tokens;

    public AuthController(UserManager<User> users, ITokenService tokens)
    {
        _users = users;
        _tokens = tokens;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto dto)
    {
        var user = new User
        {
            UserName = dto.Email,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
        };

        var result = await _users.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(error.Code, error.Description);
            return ValidationProblem(ModelState);
        }

        return Ok(ToResponse(user));
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
    {
        var user = await _users.FindByEmailAsync(dto.Email);
        if (user is null || !await _users.CheckPasswordAsync(user, dto.Password))
            return Unauthorized(new { message = "Invalid email or password." });

        return Ok(ToResponse(user));
    }

    [Authorize]
    [HttpGet("me")]
    public ActionResult<object> Me() => Ok(new
    {
        id = User.FindFirstValue(ClaimTypes.NameIdentifier),
        email = User.FindFirstValue(ClaimTypes.Email),
        firstName = User.FindFirstValue(ClaimTypes.GivenName),
        lastName = User.FindFirstValue(ClaimTypes.Surname),
    });

    private AuthResponseDto ToResponse(User user) => new()
    {
        Token = _tokens.CreateToken(user),
        Email = user.Email!,
        FirstName = user.FirstName,
        LastName = user.LastName,
    };
}
