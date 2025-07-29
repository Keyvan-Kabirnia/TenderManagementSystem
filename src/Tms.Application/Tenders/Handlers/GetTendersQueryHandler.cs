using AutoMapper;
using MediatR;
using Tms.Application.DTOs.Common;
using Tms.Application.DTOs.Tender;
using Tms.Application.Tenders.Queries;
using Tms.Domain.Interfaces;

namespace Tms.Application.Tenders.Handlers;

public class GetTendersQueryHandler(ITenderRepository tenderRepository, IMapper mapper) 
    : IRequestHandler<GetTendersQuery, PagedResult<TenderDto>>
{
    public async Task<PagedResult<TenderDto>> Handle(GetTendersQuery request, CancellationToken cancellationToken)
    {
        var tenders = await tenderRepository.GetTendersWithCategoryAndStatusAsync(request.Page, request.PageSize);
        var totalCount = await tenderRepository.GetTotalCountAsync();

        var tenderDtos = mapper.Map<IEnumerable<TenderDto>>(tenders);

        return new PagedResult<TenderDto>
        {
            Items = tenderDtos,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
} 