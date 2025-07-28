using AutoMapper;
using MediatR;
using Tms.Application.DTOs.Common;
using Tms.Application.DTOs.Vendor;
using Tms.Application.Vendors.Queries;
using Tms.Domain.Interfaces;

namespace Tms.Application.Vendors.Handlers;

public class GetVendorsQueryHandler : IRequestHandler<GetVendorsQuery, PagedResult<VendorDto>>
{
    private readonly IVendorRepository _vendorRepository;
    private readonly IMapper _mapper;

    public GetVendorsQueryHandler(IVendorRepository vendorRepository, IMapper mapper)
    {
        _vendorRepository = vendorRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<VendorDto>> Handle(GetVendorsQuery request, CancellationToken cancellationToken)
    {
        var vendors = await _vendorRepository.GetVendorsWithBidSummaryAsync(request.Page, request.PageSize);
        var totalCount = await _vendorRepository.GetTotalCountAsync();

        var vendorDtos = _mapper.Map<IEnumerable<VendorDto>>(vendors);

        return new PagedResult<VendorDto>
        {
            Items = vendorDtos,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
} 