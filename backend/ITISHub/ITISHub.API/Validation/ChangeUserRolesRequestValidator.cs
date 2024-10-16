using FluentValidation;
using ITISHub.API.Contracts;
using ITISHub.Core.Enums;

namespace ITISHub.API.Validation;

public class ChangeUserRolesRequestValidator : AbstractValidator<ChangeUserRolesRequest>
{
    public ChangeUserRolesRequestValidator()
    {
        RuleFor(x => x.Roles)
            .NotEmpty().WithMessage("Roles list cannot be empty.")
            .Must(roles => roles.All(role => Enum.IsDefined(typeof(Role), role)))
            .WithMessage("One or more roles are invalid.");
    }
}