namespace ITISHub.Core.Models;

public class Comment
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public string Content { get; set; }
    public Post Post { get; set; }
    public Guid PostId { get; set; }
    public DateTime CreatedAt { get; set; }

    private Comment(Guid id, Guid userId, User user, string content, Post post, Guid postId)
    {
        Id = id;
        UserId = userId;
        User = user;
        Content = content;
        Post = post;
        PostId = postId;
    }

    public static Comment Create(Guid id, Guid userId, User user, string content, Post post)
    {
        return new Comment(id, userId, user, content, post, post.Id);
    }
}
