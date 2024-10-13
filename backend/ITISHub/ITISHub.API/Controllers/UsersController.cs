using ITISHub.API.Contracts;
using ITISHub.Application.Services;
using ITISHub.Infrastructure.Auth;
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
    public static async Task<IResult> DeleteUsers(UsersService usersService)
    {
        await usersService.DeleteAllUsers();
        return Results.Ok("OK!");
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] CreateUserRequest request)
    {
        await _usersService.Register(request.UserName, request.Email, request.Password);

        return Ok("OK!");
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
        return Ok("Admin data!");
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        return Ok("Вы авторизированы!");
    }
}