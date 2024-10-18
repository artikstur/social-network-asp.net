using FluentValidation;
using ITISHub.API.Contracts;

namespace ITISHub.API.Validation;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(8).WithMessage("Username must be at least 8 characters long.")
            .MaximumLength(20).WithMessage("Username must not exceed 20 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is not valid.")
            .MaximumLength(20).WithMessage("Email must not exceed 20 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(20).WithMessage("Password must not exceed 20 characters.");
    }
}