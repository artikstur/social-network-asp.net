namespace ITISHub.Persistence.Entities;

public class PermissionEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<RoleEntity> Roles { get; set; } = new List<RoleEntity>();
}