using CafeTracker.Data.Models;

namespace CafeTracker.Api.Services;

public interface ITokenService
{
    string CreateToken(User user);
}