using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskTrack.Application.Interfaces;
using TaskTrack.Domain;
using TaskTrack.Domain.Entities;
using TaskTrack.Infrastructure.Entities;

namespace TaskTrack.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IJwtAuthRepository _jwtRepo;
    private readonly IPasswordHasher<UserEntity> _hasher;
    public AuthRepository(ApplicationDbContext db, IJwtAuthRepository jwtRepo, IPasswordHasher<UserEntity> hasher)
    {
        _db = db;
        _jwtRepo = jwtRepo;
        _hasher = hasher;
    }

    public async Task<JwtTokens> Login(string username, string password)
    {
        UserEntity userEntity = await _db.Users.FirstOrDefaultAsync(x => x.Username == username);
        if (userEntity == null)
        {
            return null;
        }

        if(_hasher.VerifyHashedPassword(userEntity, userEntity.PasswordHash, password) != PasswordVerificationResult.Success)
        {
            return null;
        }

        string accessToken = await _jwtRepo.GenerateToken(userEntity.Id);
        string refreshToken = await _jwtRepo.GenerateRefreshToken(userEntity.Id);
        (JwtTokens tokens, string error) = JwtTokens.Create(accessToken, refreshToken);

        if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken) || !string.IsNullOrEmpty(error))
        {
            return null;
        }

        return tokens;
    }

    public async Task<User> Register(string username, string email, string password)
    {
        if (await _db.Users.FirstOrDefaultAsync(x => x.Username == username) != null ||
            await _db.Users.FirstOrDefaultAsync(x => x.Email == email) != null)
        {
            return null;
        }

        UserEntity userEntity = new UserEntity();
        userEntity.Username = username;
        userEntity.Email = email;
        userEntity.Role = UserRoles.User;
        userEntity.PasswordHash = _hasher.HashPassword(userEntity, password);

        await _db.Users.AddAsync(userEntity);
        await _db.SaveChangesAsync();

        (User user, string error) = User.Create(userEntity.Id, userEntity.Username, userEntity.Email, userEntity.Role, userEntity.PasswordHash);
        if (!string.IsNullOrEmpty(error))
        {
            return null;
        }

        return user;
    }
}
