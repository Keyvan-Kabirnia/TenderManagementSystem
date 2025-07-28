using FluentValidation;
using Tms.Application.Vendors.Requests;

namespace Tms.Application.Vendors.Validators;

public class CreateVendorCommandValidator : AbstractValidator<CreateVendorRequest>
{
    public CreateVendorCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

        RuleFor(x => x.ContactPerson)
            .NotEmpty().WithMessage("Contact person is required")
            .MaximumLength(100).WithMessage("Contact person cannot exceed 100 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone is required")
            .MaximumLength(20).WithMessage("Phone cannot exceed 20 characters");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required")
            .MaximumLength(500).WithMessage("Address cannot exceed 500 characters");

        RuleFor(x => x.TaxNumber)
            .NotEmpty().WithMessage("Tax number is required")
            .MaximumLength(50).WithMessage("Tax number cannot exceed 50 characters");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("User is required");
    }
} 