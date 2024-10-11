using ITISHub.API.Contracts;
using ITISHub.Application.Services;
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

    [HttpPost("register-user")]
    public async Task<ActionResult> RegisterUser([FromBody] CreateUserRequest request)
    {
        await _usersService.Register(request.UserName, request.Email);

        return Ok("OK!");
    }
}