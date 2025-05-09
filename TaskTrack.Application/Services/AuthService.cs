using System;
using TaskTrack.Application.Interfaces;
using TaskTrack.Domain.Entities;
using TaskTrack.Infrastructure.Repositories;

namespace TaskTrack.Application.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepo;
    private readonly IJwtAuthRepository _jwtRepo;
    public AuthService(IAuthRepository authRepo, IJwtAuthRepository jwtRepo)
    {
        _authRepo = authRepo;
        _jwtRepo = jwtRepo;
    }

    public async Task<JwtTokens> Login(string username, string password)
    {
        return await _authRepo.Login(username, password);
    }

    public async Task<User> Register(string username, string email, string password)
    {
        return await _authRepo.Register(username, email, password);
    }

    public async Task<JwtTokens> RefreshTokens(string refreshToken)
    {
        return await _jwtRepo.RefershTokens(refreshToken);
    }
}
