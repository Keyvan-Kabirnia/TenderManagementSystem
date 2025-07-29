using FluentValidation;
using Tms.Application.Tenders.Requests;

namespace Tms.Application.Tenders.Validators;

public class CreateTenderRequestValidator : AbstractValidator<CreateTenderRequest>
{
    public CreateTenderRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters");

        RuleFor(x => x.Deadline)
            .NotEmpty().WithMessage("Deadline is required")
            .GreaterThan(DateTime.UtcNow).WithMessage("Deadline must be in the future");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Category is required");

        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage("Status is required");
    }
} 