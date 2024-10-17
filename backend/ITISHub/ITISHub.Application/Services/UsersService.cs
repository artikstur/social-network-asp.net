using ITISHub.Application.Interfaces.Auth;
using ITISHub.Application.Interfaces.Repositories;
using ITISHub.Application.Utils;
using ITISHub.Core.Enums;
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
    public async Task<Result<List<User>>> GetAllUsers()
    {
        var usersResult = await _usersRepository.GetAllUsers();

        if (!usersResult.IsSuccess)
        {
            return Result<List<User>>.Failure(usersResult.Error);
        }

        return Result<List<User>>.Success(usersResult.Value);
    }

    public async Task<Result<HashSet<Permission>>> GetPermissionsByUserId(Guid userId)
    {
        var permissionsResult = await _usersRepository.GetUserPermissions(userId);

        if (!permissionsResult.IsSuccess)
        {
            return Result<HashSet<Permission>>.Failure(permissionsResult.Error);
        }

        return Result<HashSet<Permission>>.Success(permissionsResult.Value);
    }

    public async Task<Result<string>> Login(string email, string password)
    {
        var userResult = await _usersRepository.GetByEmail(email);

        if (!userResult.IsSuccess)
        {
            return Result<string>.Failure(userResult.Error);
        }

        var user = userResult.Value;
        var passwordIsValid = _passwordHasher.Verify(password, user.PasswordHash);

        if (!passwordIsValid)
        {
            return Result<string>.Failure(new Error("Неверный пароль.", ErrorType.AuthenticationError));
        }

        var token = _jwtProvider.Generate(user);
        return Result<string>.Success(token);
    }


    public async Task<Result> Register(string userName, string email, string password)
    {
        var hashedPassword = _passwordHasher.Generate(password);

        var user = User.Create(Guid.NewGuid(), userName, hashedPassword, email);

        var addUserResult = await _usersRepository.Add(user);

        if (!addUserResult.IsSuccess)
        {
            Result.Failure(addUserResult.Error);
        }

        return Result.Success();
    }

    public async Task<Result> DeleteAllUsers()
    {
        var deleteUsersResult = await _usersRepository.DeleteAllUsers();

        if (!deleteUsersResult.IsSuccess)
        {
            return Result.Failure(deleteUsersResult.Error);
        }

        return Result.Success();
    }

    public async Task<Result<HashSet<Role>>> GetUserRolesByUserId(Guid userId)
    {
        var userRolesResult = await _usersRepository.GetUserRoles(userId);
        
        if (!userRolesResult.IsSuccess)
        {
            return Result<HashSet<Role>>.Failure(userRolesResult.Error);
        }

        return userRolesResult;
    }

    public async Task<Result> ChangeUserRolesById(Guid userId, List<Role> newRoles)
    {
        var changeUserRolesResult = await _usersRepository.ChangeUserRolesById(userId, newRoles);

        if (!changeUserRolesResult.IsSuccess)
        {
            return Result.Failure(changeUserRolesResult.Error);
        }

        return Result.Success();
    }
}