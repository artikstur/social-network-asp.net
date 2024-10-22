namespace ITISHub.Core.Models;

public class Post
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid UserId { get; set; }
    public int Likes { get; set; }

    private Post(Guid id, string content, DateTime createdAt, Guid userId, int likes)
    {
        Id = id;
        Content = content;
        CreatedAt = createdAt;
        UserId = userId;
        Likes = likes;
    }

    public static Post Create(Guid id, string content, Guid userId)
    {
        return new Post(id, content, DateTime.Now, userId, 0);
    }
}
