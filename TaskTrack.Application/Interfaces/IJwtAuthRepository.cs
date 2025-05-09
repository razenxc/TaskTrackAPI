using TaskTrack.Domain.Entities;

namespace TaskTrack.Application.Interfaces;


public interface IJwtAuthRepository
{
    Task<string> GenerateRefreshToken(Guid userId);
    Task<string> GenerateToken(Guid userId);
    Task<JwtTokens> RefershTokens(string refreshToken);
}

