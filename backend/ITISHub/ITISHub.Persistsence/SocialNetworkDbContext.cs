using ITISHub.Persistence.Configurations;
using ITISHub.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ITISHub.Persistence;

public class SocialNetworkDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<PermissionEntity> Permissions { get; set; }
    public DbSet<UserRoleEntity> UserRoles { get; set; }
    public DbSet<RolePermissionEntity> RolePermissions { get; set; }

    private readonly DbContextOptions<SocialNetworkDbContext> _options;
    private readonly IOptions<AuthorizationOptions> _authOptions;
    public SocialNetworkDbContext(DbContextOptions<SocialNetworkDbContext> options,
        IOptions<AuthorizationOptions> authOptions) : base(options)
    {
        _options = options;
        _authOptions = authOptions;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(_authOptions.Value));

        base.OnModelCreating(modelBuilder);
    }
}