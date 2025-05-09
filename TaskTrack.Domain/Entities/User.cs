using System;

namespace TaskTrack.Domain.Entities;

public class User
{
    private User(Guid id, string username, string email, string role, string passwordHash)
    {
        Id = id;
        Username = username;
        Email = email;
        Role = role;
        PasswordHash = passwordHash;
    }
    public Guid Id { get; }
    public string Username { get; }
    public string Email { get; }
    public string Role { get; }
    public string PasswordHash { get; }


    // UNSECURE: PasswordHash should be excluded from ctor
    public static (User user, string error) Create(Guid id, string username, string email, string role, string passwordHash)
    {
        string error = string.Empty;

        User model = new User(id, username, email, role, passwordHash);

        return (model, error);
    }

}
