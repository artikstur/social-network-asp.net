using FluentValidation;
using ITISHub.API.Contracts;
using ITISHub.Application.Services;
using ITISHub.Application.Utils;
using ITISHub.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITISHub.API.Controllers;

[Authorize(Policy = "RequireAdmin")]
[ApiController]
[Route("api/[controller]")]
public class AdminController: ControllerBase
{
    private readonly UsersService _usersService;
    private readonly ErrorResponseFactory _errorResponseFactory;

    public AdminController(UsersService userService, ErrorResponseFactory errorResponseFactory)
    {
        _usersService = userService;
        _errorResponseFactory = errorResponseFactory;
    }

    [HttpGet]
    public IActionResult CheckAdminAccess()
    {
        return Ok(Envelope.Ok());
    }

    [HttpGet("users")]
    public async Task<ActionResult> GetAllUsers()
    {
        var usersResult = await _usersService.GetAllUsers();

        return Ok(Envelope.Ok(usersResult.Value));
    }

    [HttpGet("manage-users")]
    public IActionResult ManageUsers()
    {
        return Ok(Envelope.Ok());
    }

    [HttpDelete("delete-users")]
    public async Task<ActionResult> DeleteUsers(UsersService usersService)
    {
        await usersService.DeleteAllUsers();

        return Ok(Envelope.Ok());
    }

    [HttpGet("role")]
    public IActionResult CheckRole()
    {
        return Ok(Envelope.Ok());
    }

    [HttpGet("{userId}/permissions")]
    public async Task<IActionResult> GetUserPermissionsByUserId(Guid userId)
    {
        var permissionsResult = await _usersService.GetPermissionsByUserId(userId);

        if (!permissionsResult.IsSuccess)
        {
            return _errorResponseFactory.CreateResponse(permissionsResult.Error);
        }

        return Ok(Envelope.Ok(permissionsResult.Value.Select(p => p.ToString())));
    }

    [HttpGet("{userId}/roles")]
    public async Task<IActionResult> GetUserRolesByUserId(Guid userId)
    {
        var rolesResult = await _usersService.GetUserRolesByUserId(userId);

        if (!rolesResult.IsSuccess)
        {
            return _errorResponseFactory.CreateResponse(rolesResult.Error);
        }

        return Ok(Envelope.Ok(rolesResult.Value.Select(p => p.ToString())));
    }

    [HttpPut("update-roles")]
    public async Task<IActionResult> GetUserRolesByUserId([FromBody] ChangeUserRolesRequest request,
        [FromServices] IValidator<ChangeUserRolesRequest> validator)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(error => new ResponseError("VALIDATION_ERROR", error.ErrorMessage, error.PropertyName))
                .ToList();

            return BadRequest(Envelope.Error(errors));
        }

        var roles = request.Roles.Select(Enum.Parse<Role>).ToList();

        var changeRolesResult = await _usersService.ChangeUserRolesById(request.UserId, roles);

        if (!changeRolesResult.IsSuccess)
        {
            return _errorResponseFactory.CreateResponse(changeRolesResult.Error);
        }

        return Ok(Envelope.Ok());
    }
}