using FluentValidation;
using ITISHub.API.Contracts;
using ITISHub.API.Contracts.Responses;
using ITISHub.Application.Services;
using ITISHub.Application.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ITISHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersService _usersService;
    private readonly ErrorResponseFactory _errorResponseFactory;

    public UsersController(UsersService userService, ErrorResponseFactory errorResponseFactory)
    {
        _usersService = userService;
        _errorResponseFactory = errorResponseFactory;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(
        [FromBody] CreateUserRequest request,
        [FromServices] IValidator<CreateUserRequest> validator)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(error => new ResponseError("VALIDATION_ERROR", error.ErrorMessage, error.PropertyName))
                .ToList();

            return BadRequest(Envelope.Error(errors));
        }
        
        var registerResult = await _usersService.Register(request.UserName, request.Email, request.Password);

        if (!registerResult.IsSuccess)
        {
            return _errorResponseFactory.CreateResponse(registerResult.Error);
        }

        return Ok(Envelope.Ok());
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        var loginResult = await _usersService.Login(request.Email, request.Password);

        if (!loginResult.IsSuccess)
        {
            return _errorResponseFactory.CreateResponse(loginResult.Error);
        }

        var token = loginResult.Value;
        Response.Cookies.Append("tasty-cookies", token);

        return Ok(Envelope.Ok(token));
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetUserByUserName(string userName, [FromServices] IValidator<GetUserByUserNameRequest> validator)
    {
        var validationResult = await validator.ValidateAsync( new GetUserByUserNameRequest(userName));

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(error => new ResponseError("VALIDATION_ERROR", error.ErrorMessage, error.PropertyName))
                .ToList();

            return BadRequest(Envelope.Error(errors));
        }

        var userResult = await _usersService.GetUserByUserName(userName);

        if (!userResult.IsSuccess)
        {
            return _errorResponseFactory.CreateResponse(userResult.Error);
        }

        return Ok(Envelope.Ok(new GetUserByUserNameResponse(userResult.Value.Id, userResult.Value.UserName, userResult.Value.Email)));
    }
}