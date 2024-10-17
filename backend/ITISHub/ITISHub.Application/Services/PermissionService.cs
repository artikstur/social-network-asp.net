using ITISHub.Application.Interfaces.Auth;
using ITISHub.Application.Interfaces.Repositories;
using ITISHub.Application.Utils;
using ITISHub.Core.Enums;

namespace ITISHub.Application.Services;

public class PermissionService : IPermissionService
{
    private readonly IUsersRepository _usersRepository;

    public PermissionService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<Result<HashSet<Permission>>> GetPermissionsAsync(Guid userId)
    {
        var permissionsResult = await _usersRepository.GetUserPermissions(userId);

        if (!permissionsResult.IsSuccess)
        {
            return Result<HashSet<Permission>>.Failure(permissionsResult.Error);
        }

        return Result<HashSet<Permission>>.Success(permissionsResult.Value);
    }
}
