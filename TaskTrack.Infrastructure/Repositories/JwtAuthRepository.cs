using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TaskTrack.Application.Interfaces;
using TaskTrack.Domain.Entities;
using TaskTrack.Infrastructure.Entities;

namespace TaskTrack.Infrastructure.Repositories;

public class JwtAuthRepository : IJwtAuthRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IConfiguration _config;
    public JwtAuthRepository(ApplicationDbContext db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }
    public async Task<string> GenerateToken(Guid userId)
    {
        UserEntity user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if(user == null)
        {
            return string.Empty;
        }

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SigningKey"]));
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        JwtSecurityToken token = new JwtSecurityToken
        (
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config["Jwt:TokenExpiresInMin"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> GenerateRefreshToken(Guid userId)
    {
        if (await _db.Users.FirstOrDefaultAsync(x => x.Id == userId) == null)
        {
            return null;
        }

        JwtRefreshTokenEntity jwtRefreshToken = new JwtRefreshTokenEntity
        {
            UserId = userId,
            RefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.UtcNow.AddDays(Convert.ToInt32(_config["Jwt:RefreshTokenExpiresInDays"]))
        };

        await _db.JwtRefreshTokens.AddAsync(jwtRefreshToken);
        await _db.SaveChangesAsync();

        return jwtRefreshToken.RefreshToken;
    }

    public async Task<JwtTokens> RefershTokens(string refreshToken)
    {
        JwtRefreshTokenEntity jwtRefreshTokenEntity = await _db.JwtRefreshTokens.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
        if (jwtRefreshTokenEntity == null)
        {
            return null;
        }

        UserEntity user = await _db.Users.FirstOrDefaultAsync(x => x.Id == jwtRefreshTokenEntity.UserId);
        if (user == null)
        {
            return null;
        }

        string newAccessToken = await GenerateToken(jwtRefreshTokenEntity.UserId);
        string newRefershToken = await GenerateRefreshToken(jwtRefreshTokenEntity.UserId);

        (JwtTokens jwtTokens, string error) = JwtTokens.Create(newAccessToken, newRefershToken);
        if (!string.IsNullOrEmpty(error))
        {
            return null;
        }

        return jwtTokens;
    }
}
