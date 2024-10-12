using ITISHub.Core.Enums;
using Microsoft.AspNetCore.Authorization;

namespace ITISHub.Infrastructure.Auth;

public class PermissionRequirement: IAuthorizationRequirement
{
    public Permission[] Permissions { get; }

    public PermissionRequirement(params Permission[] permissions)
    {
        Permissions = permissions;
    }
}
