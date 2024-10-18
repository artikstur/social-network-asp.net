using FluentValidation;
using ITISHub.API.Contracts;
using ITISHub.Application.Services;
using ITISHub.Application.Utils;
using ITISHub.Core.Enums;
using Microsoft.AspNetCore.Authorization;
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

    //[HttpGet("admin")]
    //[Authorize(Policy = "RequireAdmin")]
    //public IActionResult GetAdminData()
    //{
    //    return Ok(Envelope.Ok("Success! Only admins"));
    //}

    //[HttpGet("profile")]
    //[Authorize]
    //public async Task<IActionResult> GetProfile()
    //{
    //    return Ok(Envelope.Ok("Success! You authenticated"));
    //}

    //[HttpGet("{userId}/permissions")]
    //public async Task<IActionResult> GetUserPermissionsByUserId(Guid userId)
    //{
    //    var permissionsResult = await _usersService.GetPermissionsByUserId(userId);

    //    if (!permissionsResult.IsSuccess)
    //    {
    //        return _errorResponseFactory.CreateResponse(permissionsResult.Error);
    //    }

    //    return Ok(Envelope.Ok(permissionsResult.Value.Select(p => p.ToString())));
    //}

    //[HttpGet("{userId}/roles")]
    //public async Task<IActionResult> GetUserRolesByUserId(Guid userId)
    //{
    //    var rolesResult = await _usersService.GetUserRolesByUserId(userId);

    //    if (!rolesResult.IsSuccess)
    //    {
    //        return _errorResponseFactory.CreateResponse(rolesResult.Error);
    //    }

    //    return Ok(Envelope.Ok(rolesResult.Value.Select(p => p.ToString())));
    //}

    //[HttpGet]
    //public async Task<ActionResult> GetAllUsers()
    //{
    //    var usersResult = await _usersService.GetAllUsers();

    //    return Ok(Envelope.Ok(usersResult.Value));
    //}
}