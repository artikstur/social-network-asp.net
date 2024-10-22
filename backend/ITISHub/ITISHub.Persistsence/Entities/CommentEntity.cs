namespace ITISHub.Persistence.Entities;

public class CommentEntity
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public Guid PostId { get; set; }
    public PostEntity Post { get; set; }
}
