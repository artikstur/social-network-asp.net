using ITISHub.Application.Interfaces.Repositories;
using ITISHub.Application.Utils;
using ITISHub.Core.Models;

namespace ITISHub.Application.Services;

public class PostsService
{
    private readonly IPostsRepository _postsRepository;
    public PostsService(IPostsRepository postsRepository)
    {
        _postsRepository = postsRepository;
    }

    public async Task<Result> CreatePost(string content, Guid userId)
    {
        var postCreateResult = await _postsRepository.CreatePost(content, userId);

        if (!postCreateResult.IsSuccess)
        {
            return Result.Failure(postCreateResult.Error);
        }

        return postCreateResult;
    }

    public async Task<Result<ICollection<Post>>> GetAllPosts()
    {
        var postsResult = await _postsRepository.GetAllPosts();

        if (!postsResult.IsSuccess)
        {
            return Result<ICollection<Post>>.Failure(postsResult.Error);
        }

        return Result<ICollection<Post>>.Success(postsResult.Value);
    }

    public async Task<Result<ICollection<Post>>> GetAllUserPosts(Guid userId)
    {
        var postsResult = await _postsRepository.GetAllUserPosts(userId);

        if (!postsResult.IsSuccess)
        {
            return Result<ICollection<Post>>.Failure(postsResult.Error);
        }

        return Result<ICollection<Post>>.Success(postsResult.Value);
    }

    public async Task<Result> EditPostText(string content, Guid postId)
    {
        var editPostResult = await _postsRepository.EditTextById(content, postId);

        if (!editPostResult.IsSuccess)
        {
            return Result.Failure(editPostResult.Error);
        }

        return editPostResult;
    }
}