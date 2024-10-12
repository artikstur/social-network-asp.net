namespace ITISHub.API.Contracts;

public record CreateUserRequest(string UserName, string Email, string Password);