namespace ITISHub.API.Contracts;

public record ChangeUserRolesRequest(Guid UserId, List<string> Roles);