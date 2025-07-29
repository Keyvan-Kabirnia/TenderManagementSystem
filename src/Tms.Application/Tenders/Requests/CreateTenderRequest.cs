using Tms.Application.Common;
using Tms.Application.DTOs.Tender;

namespace Tms.Application.Tenders.Requests;

public record CreateTenderRequest : IRequest<TenderDto>
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime Deadline { get; init; }
    public int CategoryId { get; init; }
    public int StatusId { get; init; }
} 