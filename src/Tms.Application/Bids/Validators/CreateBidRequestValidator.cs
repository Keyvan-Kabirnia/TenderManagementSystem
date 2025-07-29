using FluentValidation;
using Tms.Application.Bids.Requests;

namespace Tms.Application.Bids.Validators;

public class CreateBidRequestValidator : AbstractValidator<CreateBidRequest>
{
    public CreateBidRequestValidator()
    {
        RuleFor(x => x.TenderId)
            .GreaterThan(0).WithMessage("Tender is required");

        RuleFor(x => x.VendorId)
            .GreaterThan(0).WithMessage("Vendor is required");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0");

        RuleFor(x => x.Comments)
            .MaximumLength(1000).WithMessage("Comments cannot exceed 1000 characters");
    }
} 