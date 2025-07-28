using FluentValidation;
using Tms.Application.Tenders.Requests;

namespace Tms.Application.Tenders.Validators;

public class CreateTenderCommandValidator : AbstractValidator<CreateTenderRequest>
{
    public CreateTenderCommandValidator()
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

        RuleFor(x => x.EstimatedBudget)
            .GreaterThan(0).When(x => x.EstimatedBudget.HasValue)
            .WithMessage("Estimated budget must be greater than 0");

        RuleFor(x => x.Requirements)
            .MaximumLength(5000).WithMessage("Requirements cannot exceed 5000 characters");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Category is required");

        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage("Status is required");
    }
} 