using ITISHub.Core.Enums;

namespace ITISHub.Application.Interfaces.Auth;

public interface IPermissionService
{
    Task<HashSet<Permission>> GetPermissionsAsync(Guid userId);
}
