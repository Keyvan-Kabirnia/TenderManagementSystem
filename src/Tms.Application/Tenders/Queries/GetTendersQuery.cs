using Tms.Application.Common;
using Tms.Application.DTOs.Common;
using Tms.Application.DTOs.Tender;

namespace Tms.Application.Tenders.Queries;

public record GetTendersQuery : IRequest<PagedResult<TenderDto>>
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
} 