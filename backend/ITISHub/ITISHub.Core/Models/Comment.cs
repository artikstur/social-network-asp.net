namespace ITISHub.Core.Models;

public class Comment
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public string Text { get; set; }

    private Comment(Guid id, Guid userId, User user, string text)
    {
        Id = id;
        UserId = userId;
        User = user;
        Text = text;
    }

    public static Comment Create(Guid userId, User user, string text)
    {
        var id = Guid.NewGuid();

        return new Comment(id, userId, user, text);
    }
}