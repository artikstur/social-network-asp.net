using ITISHub.Application.Interfaces.Repositories;
using ITISHub.Core.Models;

namespace ITISHub.Application.Services;

public class UsersService
{
    private readonly IUsersRepository _usersRepository;
    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    public async Task<List<User>> GetAllUsers()
    {
        var users = await _usersRepository.GetAllUsers();

        return users;
    }

    public async Task Register(string username, string email)
    {
        var user = User.Create(Guid.NewGuid(), username, "hashedPassword", email);
        
        await _usersRepository.Add(user);
    }
}