using ITISHub.API.Contracts;
using ITISHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ITISHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController: ControllerBase
{
    private readonly PostsService _postsService;
    private readonly ErrorResponseFactory _errorResponseFactory; 

    public PostsController(PostsService postsService, ErrorResponseFactory errorResponseFactory)
    {
        _postsService = postsService;
        _errorResponseFactory = errorResponseFactory;
    }

    // TODO: Добавить картинки и видео в посты

    [HttpPost("create-post")]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest request)
    {
        // TODO: Добавить валидацию и прикрутить Envelope

        var result = await _postsService.CreatePost(request.Content, request.UserId);

        if (!result.IsSuccess)
        {
            return _errorResponseFactory.CreateResponse(result.Error);
        }

        return Ok(result);
    }

    [HttpGet("all-posts")]
    public async Task<IActionResult> GetAllPosts()
    {
        // TODO: Добавить валидацию и прикрутить Envelope

        var postsResult = await _postsService.GetAllPosts();

        if (!postsResult.IsSuccess)
        {
            return _errorResponseFactory.CreateResponse(postsResult.Error);
        }

        return Ok(postsResult.Value);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAllUserPosts(Guid userId)
    {
        // TODO: Добавить валидацию и прикрутить Envelope

        var postsResult = await _postsService.GetAllUserPosts(userId);

        if (!postsResult.IsSuccess)
        {
            return _errorResponseFactory.CreateResponse(postsResult.Error);
        }

        return Ok(postsResult.Value);
    }

    [HttpPost("edit-post-text")]
    public async Task<IActionResult> EditPostText([FromBody] EditPostTextRequest request)
    {
        // TODO: Добавить валидацию и прикрутить Envelope

        var editPostResult = await _postsService.EditPostText(request.Content, request.PostId);

        if (!editPostResult.IsSuccess)
        {
            return _errorResponseFactory.CreateResponse(editPostResult.Error);
        }

        return Ok();
    }
}
