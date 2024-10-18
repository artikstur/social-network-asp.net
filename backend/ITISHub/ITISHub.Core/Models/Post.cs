namespace ITISHub.Core.Models;

public class Post
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public List<Resource>? Resources { get; set; }
    public DateTime CreatedAt { get; set; } 
    public Guid UserId { get; set; }
    public User User { get; set; }
    public int Likes { get; set; }

    private Post(Guid id, string content, List<Resource>? resources, DateTime createdAt, Guid userId, User user, int likes)
    {
        Id = id;
        Content = content;
        Resources = resources;
        CreatedAt = createdAt;
        UserId = userId;
        User = user;
        Likes = likes;
    }

    public static Post Create(string content, List<Resource>? resources, Guid userId, User user)
    {
        var id = Guid.NewGuid();
        var createdAt = DateTime.Now; 
        var likes = 0; 

        return new Post(id, content, resources, createdAt, userId, user, likes);
    }
}
