using Tms.Application.Common;
using Tms.Application.DTOs.Tender;

namespace Tms.Application.Tenders.Requests;

public record UpdateTenderRequest : IRequest<TenderDto>
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime Deadline { get; init; }
    public decimal? EstimatedBudget { get; init; }
    public string Requirements { get; init; } = string.Empty;
    public int CategoryId { get; init; }
    public int StatusId { get; init; }
    public bool IsActive { get; init; }
} 