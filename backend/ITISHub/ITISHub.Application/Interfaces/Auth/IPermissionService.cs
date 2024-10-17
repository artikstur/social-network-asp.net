using ITISHub.Application.Utils;
using ITISHub.Core.Enums;

namespace ITISHub.Application.Interfaces.Auth;

public interface IPermissionService
{
    Task<Result<HashSet<Permission>>> GetPermissionsAsync(Guid userId);
}
