using ITISHub.Application.Utils;
using ITISHub.Core.Enums;
using ITISHub.Core.Models;

namespace ITISHub.Application.Interfaces.Repositories;

public interface IUsersRepository
{
    Task<Result> Add(User user);
    Task<Result<User>> GetById(Guid userId);
    Task<Result<User>> GetByEmail(string email);
    Task<Result<List<User>>> GetAllUsers();
    Task<Result<HashSet<Permission>>> GetUserPermissions(Guid userId);
    Task<Result<HashSet<Role>>> GetUserRoles(Guid userId);
    Task<Result> DeleteAllUsers();
    Task<Result> ChangeUserRolesById(Guid userId, List<Role> newRoles);
    Task<Result<User>> GetByUserName(string userName);
}