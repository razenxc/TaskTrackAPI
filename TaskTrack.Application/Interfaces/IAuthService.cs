using TaskTrack.Domain.Entities;

namespace TaskTrack.Application.Services;

public interface IAuthService
{
    Task<JwtTokens> Login(string username, string password);
    Task<JwtTokens> RefreshTokens(string refreshToken);
    Task<User> Register(string username, string email, string password);
}
