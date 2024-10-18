namespace ITISHub.API.Contracts.Responses;

public record GetUserByUserNameResponse(Guid Id, string UserName, string Email);