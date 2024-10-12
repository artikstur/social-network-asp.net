using ITISHub.Application.Interfaces.Auth;
using ITISHub.Application.Interfaces.Repositories;
using ITISHub.Core.Models;

namespace ITISHub.Application.Services;

public class UsersService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtProvider _jwtProvider;
    public UsersService(
        IUsersRepository usersRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
    {
        _passwordHasher = passwordHasher;
        _usersRepository = usersRepository;
        _jwtProvider = jwtProvider;
    }
    public async Task<List<User>> GetAllUsers()
    {
        var users = await _usersRepository.GetAllUsers();

        return users;
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await _usersRepository.GetByEmail(email);

        var result = _passwordHasher.Verify(password, user.PasswordHash);

        if (!result)
        {
            throw new Exception("Failed to LOGIN!!!");
        }

        var token = _jwtProvider.Generate(user);

        return token;
    }

    public async Task Register(string userName, string email, string password)
    {
        var hashedPassword = _passwordHasher.Generate(password);

        var user = User.Create(Guid.NewGuid(), userName, hashedPassword, email);

        await _usersRepository.Add(user);
    }

    public async Task<List<User>> GetUsers()
    {
        var users = await _usersRepository.GetAllUsers();

        return users;
    }

    public async Task DeleteAllUsers()
    {
        await _usersRepository.DeleteAllUsers();
    }
}