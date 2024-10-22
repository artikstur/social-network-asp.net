using ITISHub.Application.Utils;
using ITISHub.Core.Models;

namespace ITISHub.Application.Interfaces.Repositories;

public interface IPostsRepository
{
    Task<Result> CreatePost(string content, Guid userId);
    Task<Result> DeletePost(Guid postId);
    Task<Result<Post>> GetById(Guid postId);
    Task<Result<ICollection<Post>>> GetAllPosts();
    Task<Result<ICollection<Post>>> GetAllUserPosts(Guid userId);
    Task<Result> EditTextById(string content, Guid postId);
}