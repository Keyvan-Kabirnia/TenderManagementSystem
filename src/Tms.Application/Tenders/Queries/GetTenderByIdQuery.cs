using Tms.Application.Common;
using Tms.Application.DTOs.Tender;

namespace Tms.Application.Tenders.Queries;

public record GetTenderByIdQuery : IRequest<TenderDetailDto?>
{
    public int Id { get; init; }
} 