using System;

namespace TaskTrack.Domain.Entities;

public class JwtRefreshToken
{
    private JwtRefreshToken(Guid id, Guid userId, string refreshToken, DateTime expires)
    {
        Id = id;
        UserId = userId;
        RefreshToken = refreshToken;
        Expires = expires;
    }

    public Guid Id { get; }
    public Guid UserId { get; }
    public string RefreshToken { get; }
    public DateTime Expires { get; }

    public static (JwtRefreshToken jwtRefreshToken, string Error) Create(Guid id, Guid userId, string refreshToken, DateTime expires)
    {
        string error = string.Empty;

        if(expires < DateTime.UtcNow || expires < DateTime.Now)
        {
            error = "Expiration date cannot be in past!";
            return (null, error);
        }

        JwtRefreshToken model = new JwtRefreshToken(id, userId, refreshToken, expires);

        return (model, error);
    }
}
