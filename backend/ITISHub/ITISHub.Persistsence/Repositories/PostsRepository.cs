using AutoMapper;
using ITISHub.Application.Interfaces.Repositories;
using ITISHub.Application.Utils;
using ITISHub.Core.Enums;
using ITISHub.Core.Models;
using ITISHub.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITISHub.Persistence.Repositories;

public class PostsRepository : IPostsRepository
{
    private readonly SocialNetworkDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IUsersRepository _usersRepository;

    public PostsRepository(SocialNetworkDbContext dbContext,
        IUsersRepository usersRepository,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _usersRepository = usersRepository;
    }

    public async Task<Result> CreatePost(string content, Guid userId)
    {
        var userEntity = await _dbContext.Users.FindAsync(userId);

        if (userEntity == null)
        {
            return Result.Failure(new Error("Пользователь не найден", ErrorType.ServerError));
        }

        var postEntity = new PostEntity()
        {
            Id = Guid.NewGuid(),
            Content = content,
            CreatedAt = DateTime.UtcNow,
            UserId = userId,
            User = userEntity,
            Likes = 0,
            Resources = new List<ResourceEntity>(),
            Comments = new List<CommentEntity>(),
        };

        await _dbContext.Posts.AddAsync(postEntity);

        await _dbContext.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> DeletePost(Guid postId)
    {
        var postEntity = await _dbContext.Posts
            .FirstOrDefaultAsync(p => p.Id == postId);

        if (postEntity == null)
        {
            return Result.Failure(new Error("Пост не найден", ErrorType.ServerError));
        }

        _dbContext.Posts.Remove(postEntity);

        await _dbContext.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<Post>> GetById(Guid postId)
    {
        var postEntity = await _dbContext.Posts
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == postId);

        if (postEntity == null)
        {
            return Result<Post>.Failure(new Error("Пост не найден", ErrorType.ServerError));
        }

        var userResult = await _usersRepository.GetById(postEntity.UserId);

        if (!userResult.IsSuccess)
        {
            return Result<Post>.Failure(new Error("Пользователь не найден", ErrorType.ServerError));
        }

        var post = Post.Create(postEntity.Id, postEntity.Content, userResult.Value.Id);

        return Result<Post>.Success(post);
    }

    public async Task<Result<ICollection<Post>>> GetAllPosts()
    {
        var posts = await _dbContext.Posts
            .AsNoTracking()
            .Select(p => Post.Create(p.Id, p.Content, p.UserId))
            .ToListAsync();

        return Result<ICollection<Post>>.Success(posts);
    }

    public async Task<Result<ICollection<Post>>> GetAllUserPosts(Guid userId)
    {
        var userEntity = await _dbContext.Users.FindAsync(userId);

        if (userEntity == null)
        {
            return Result<ICollection<Post>>.Failure(new Error("Пользователь не найден", ErrorType.ServerError));
        }

        var posts = await _dbContext.Posts
            .AsNoTracking()
            .Where(p => p.UserId == userEntity.Id)
            .Select(p => Post.Create(p.Id, p.Content, p.UserId))
            .ToListAsync();

        return Result<ICollection<Post>>.Success(posts);
    }

    public async Task<Result> EditTextById(string content, Guid postId)
    {
        var postEntity = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);

        if (postEntity == null)
        {
            return Result.Failure(new Error("Пост не найден", ErrorType.ServerError));
        }

        postEntity.Content = content;

        await _dbContext.SaveChangesAsync();

        return Result.Success();
    }
}