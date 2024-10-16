using ITISHub.Core.Enums;
using ITISHub.Core.Models;

namespace ITISHub.Application.Interfaces.Repositories;

public interface IUsersRepository
{
    Task Add(User user);
    Task<User> GetByEmail(string email);
    Task<List<User>> GetAllUsers();
    Task<HashSet<Permission>> GetUserPermissions(Guid userId);
    Task<HashSet<Role>> GetUserRoles(Guid userId);
    Task DeleteAllUsers();
    Task ChangeUserRolesById(Guid userId, List<Role> newRoles);
}