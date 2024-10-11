namespace ITISHub.Core.Models;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }

    private User(Guid id, string userName, string passwordHash, string email)
    {
        Id = id;
        UserName = userName;
        PasswordHash = passwordHash;
        Email = email;
    }

    public static User Create(Guid id, string userName, string passwordHash, string email)
    {
        return new User(id, userName, passwordHash, email);
    }
}