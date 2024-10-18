using FluentValidation;
using ITISHub.API.Contracts;

namespace ITISHub.API.Validation;

public class GetUserByUserNameRequestValidator: AbstractValidator<GetUserByUserNameRequest>
{
    public GetUserByUserNameRequestValidator()
    {
        RuleFor(r => r.UserName)
            .NotEmpty().WithMessage("Username cannot be empty.")
            .MinimumLength(3).WithMessage("Username must be at least 8 characters long.")
            .MaximumLength(20).WithMessage("Username must not exceed 20 characters.")
            .Matches("^[a-zA-Z0-9_]+$").WithMessage("Username can only contain letters, numbers, and underscores.");
    }
}