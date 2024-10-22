namespace ITISHub.Persistence.Entities;

public class PostEntity
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public int Likes { get; set; }
    public ICollection<ResourceEntity>? Resources { get; set; }
    public ICollection<CommentEntity>? Comments { get; set; }
}
