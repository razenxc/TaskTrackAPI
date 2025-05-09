namespace TaskTrack.API.Contracts;

public record UserResponse(
    Guid Id,
    string Username,
    string Email,
    string Role
);
