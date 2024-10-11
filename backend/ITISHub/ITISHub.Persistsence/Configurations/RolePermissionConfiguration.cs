using ITISHub.Core.Enums;
using ITISHub.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITISHub.Persistence.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionEntity>
{
    private readonly AuthorizationOptions _authorizationOptions;

    public RolePermissionConfiguration(AuthorizationOptions authorizationOptions)
    {
        _authorizationOptions = authorizationOptions;
    }

    public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
    {
        // Устанавливает комбинированный ключ из полей RoleId и
        // PermissionId для таблицы RolePermissionEntity, что отображения связи "многие ко многим".
        builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });

        // Эта функция перебирает все роли и связанные с ними разрешения, создавая объекты RolePermissionEntity
        builder.HasData(ParseRolePermission());
    }

    private List<RolePermissionEntity> ParseRolePermission()
    {
        // для каждой роли создаются свои permissions
        return _authorizationOptions.RolePermissions
            .SelectMany(rp => rp.Permissions
                .Select(p => new RolePermissionEntity
                {
                    RoleId = (int)Enum.Parse<Role>(rp.Role),
                    PermissionId = (int)Enum.Parse<Permission>(p)
                }))
            .ToList();
    }
}