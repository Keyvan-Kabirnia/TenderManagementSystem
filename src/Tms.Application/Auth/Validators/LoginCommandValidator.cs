using FluentValidation;
using Tms.Application.Auth.Requests;

namespace Tms.Application.Auth.Validators;

public class LoginCommandValidator : AbstractValidator<LoginRequest>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("UserName is required")
            .MaximumLength(50).WithMessage("UserName cannot exceed 50 characters");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters");
    }
} 