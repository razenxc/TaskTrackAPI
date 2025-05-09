namespace TaskTrack.API.Contracts;

public record UserLoginRequest(
    string Username,
    string Password
);