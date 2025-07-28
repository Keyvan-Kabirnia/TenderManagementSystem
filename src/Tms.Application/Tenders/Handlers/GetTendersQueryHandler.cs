using AutoMapper;
using MediatR;
using Tms.Application.DTOs.Common;
using Tms.Application.DTOs.Tender;
using Tms.Application.Tenders.Queries;
using Tms.Domain.Interfaces;

namespace Tms.Application.Tenders.Handlers;

public class GetTendersQueryHandler : IRequestHandler<GetTendersQuery, PagedResult<TenderDto>>
{
    private readonly ITenderRepository _tenderRepository;
    private readonly IMapper _mapper;

    public GetTendersQueryHandler(ITenderRepository tenderRepository, IMapper mapper)
    {
        _tenderRepository = tenderRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<TenderDto>> Handle(GetTendersQuery request, CancellationToken cancellationToken)
    {
        var tenders = await _tenderRepository.GetTendersWithCategoryAndStatusAsync(request.Page, request.PageSize);
        var totalCount = await _tenderRepository.GetTotalCountAsync();

        var tenderDtos = _mapper.Map<IEnumerable<TenderDto>>(tenders);

        return new PagedResult<TenderDto>
        {
            Items = tenderDtos,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
} 