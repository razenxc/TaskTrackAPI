using System;

namespace TaskTrack.Domain.Entities;

public class JwtTokens
{
    public JwtTokens(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
    public string AccessToken { get; }
    public string RefreshToken { get; }

    public static (JwtTokens tokens, string Error) Create(string accessToken, string refreshToken)
    {
        string error = string.Empty;

        JwtTokens model = new JwtTokens(accessToken, refreshToken);

        return (model, error);
    }
}
