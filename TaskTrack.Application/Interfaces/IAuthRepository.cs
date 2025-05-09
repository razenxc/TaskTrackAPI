using TaskTrack.Domain.Entities;

namespace TaskTrack.Infrastructure.Repositories;

public interface IAuthRepository
{
    Task<JwtTokens> Login(string username, string password);
    Task<User> Register(string username, string email, string password);
}
