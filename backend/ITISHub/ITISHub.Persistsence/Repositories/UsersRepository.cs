using AutoMapper;
using ITISHub.Application.Interfaces.Repositories;
using ITISHub.Core.Enums;
using ITISHub.Core.Models;
using ITISHub.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ITISHub.Persistence.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly SocialNetworkDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<UsersRepository> _logger;

    public UsersRepository(SocialNetworkDbContext dbContext, IMapper mapper, ILogger<UsersRepository> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Add(User user)
    {
        var roleEntity = await _dbContext.Roles
            .SingleOrDefaultAsync(r => r.Id == (int)Role.Admin);

        var userEntity = new UserEntity()
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            Roles = new List<RoleEntity> { roleEntity },
        };

        await _dbContext.Users.AddAsync(userEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User> GetByEmail(string email)
    {
        var userEntity = await _dbContext.Users
                             .AsNoTracking()
                             .FirstOrDefaultAsync(u => u.Email == email)
                         ?? throw new Exception();

        return _mapper.Map<User>(userEntity);
    }

    public async Task<List<User>> GetAllUsers()
    {
        var userEntities = await _dbContext.Users
            .AsNoTracking()
            .ToListAsync();

        return userEntities
            .Select(userEntity => _mapper.Map<User>(userEntity))
            .ToList();
    }

    public async Task DeleteAllUsers()
    {
        var users = await _dbContext.Users.ToListAsync();

        _dbContext.Users.RemoveRange(users);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<HashSet<Permission>> GetUserPermissions(Guid userId)
    {
        var roles = await _dbContext.Users
            .AsNoTracking()
            .Include(u => u.Roles)
            .ThenInclude(r => r.Permissions)
            .Where(u => u.Id == userId)
            .Select(u => u.Roles)
            .ToArrayAsync();

        var res = roles
            .SelectMany(r => r)
            .SelectMany(r => r.Permissions)
            .Select(p => (Permission)p.Id)
            .ToHashSet();

        return res;
    }

    public async Task<HashSet<Role>> GetUserRoles(Guid userId)
    {
        var rolesList = await _dbContext.Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Roles)
            .Select(r => (Role)r.Id)
            .ToArrayAsync();

        return rolesList.ToHashSet();
    }

    public async Task ChangeUserRolesById(Guid userId, List<Role> newRoles)
    {
        var newRoleIds = newRoles.Select(r => (int)r).ToList();

        var rolesEntities = await _dbContext.Roles
            .Where(r => newRoleIds.Contains(r.Id))
            .ToListAsync();

        var userEntity = await _dbContext.Users
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (userEntity != null)
        {
            userEntity.Roles.Clear();
            foreach (var role in rolesEntities)
            {
                userEntity.Roles.Add(role);
            }
        }
    
        await _dbContext.SaveChangesAsync();
    }
}