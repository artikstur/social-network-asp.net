namespace ITISHub.Persistence.Entities;

public class UserEntity
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public ICollection<RoleEntity> Roles { get; set; } = new List<RoleEntity>();
    public ICollection<PostEntity> Posts { get; set; } = new List<PostEntity>();
    public ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
}