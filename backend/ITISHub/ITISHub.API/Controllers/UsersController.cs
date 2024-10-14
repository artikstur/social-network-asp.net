using FluentValidation;
using ITISHub.API.Contracts;
using ITISHub.API.Utils;
using ITISHub.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITISHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersService _usersService;

    public UsersController(UsersService userService)
    {
        _usersService = userService;
    }

    [HttpGet("users")]
    public async Task<ActionResult> GetAllUsers()
    {
        var users = await _usersService.GetAllUsers();

        return Ok(users);
    }

    [HttpDelete("delete-users")]
    public async Task<ActionResult> DeleteUsers(UsersService usersService)
    {
        await usersService.DeleteAllUsers();

        return Ok(Envelope.Ok());
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser(
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

        var result = _usersService.Register(request.UserName, request.Email, request.Password);

        return Ok(Envelope.Ok(result));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        var token = await _usersService.Login(request.Email, request.Password);

        if (token == null)
        {
            return Unauthorized("Invalid email or password.");
        }

        Response.Cookies.Append("tasty-cookies", token);

        return Ok(new { Token = token });
    }

    [HttpGet("admin")]
    [Authorize(Policy = "RequireAdmin")]
    public IActionResult GetAdminData()
    {
        return Ok(Envelope.Ok("Success! Only admins"));
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        return Ok(Envelope.Ok("Success! You authenticated"));
    }
}