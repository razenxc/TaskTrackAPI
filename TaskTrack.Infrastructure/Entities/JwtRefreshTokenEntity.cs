using System;

namespace TaskTrack.Infrastructure.Entities;

public class JwtRefreshTokenEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expires { get; set; }
}
