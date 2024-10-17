using ITISHub.Application.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ITISHub.Infrastructure.Auth;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<PermissionAuthorizationHandler> _logger;

    public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory, ILogger<PermissionAuthorizationHandler> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    // Этот метод отвечает за обработку логики авторизации,
    // проверяя, удовлетворяет ли текущий пользователь требованиям авторизации
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userId = context.User.Claims.FirstOrDefault(
            c => c.Type == CustomClaims.UserId);


        if (userId is null || !Guid.TryParse(userId.Value, out var id))
        {
            return;
        }

        using var scope = _serviceScopeFactory.CreateScope();

        var permissionService = scope.ServiceProvider
            .GetRequiredService<IPermissionService>();

        var permissions = await permissionService.GetPermissionsAsync(id);

        if (permissions.Value.Intersect(requirement.Permissions).Any())
        {
            context.Succeed(requirement);
        }
    }
}
