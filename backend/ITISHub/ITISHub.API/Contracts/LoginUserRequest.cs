using System.ComponentModel.DataAnnotations;

namespace ITISHub.API.Contracts;

public record LoginUserRequest(
      [Required] string Email,
      [Required] string Password);