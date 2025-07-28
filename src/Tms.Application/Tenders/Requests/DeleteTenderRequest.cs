using Tms.Application.Common;

namespace Tms.Application.Tenders.Requests;

public record DeleteTenderRequest : IRequest<bool>
{
    public int Id { get; init; }
} 