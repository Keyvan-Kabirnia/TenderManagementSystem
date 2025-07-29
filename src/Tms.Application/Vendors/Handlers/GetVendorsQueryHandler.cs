using AutoMapper;
using MediatR;
using Tms.Application.DTOs.Common;
using Tms.Application.DTOs.Vendor;
using Tms.Application.Vendors.Queries;
using Tms.Domain.Interfaces;

namespace Tms.Application.Vendors.Handlers;

public class GetVendorsQueryHandler(IVendorRepository vendorRepository, IMapper mapper) 
    : IRequestHandler<GetVendorsQuery, PagedResult<VendorDto>>
{
    public async Task<PagedResult<VendorDto>> Handle(GetVendorsQuery request, CancellationToken cancellationToken)
    {
        var vendors = await vendorRepository.GetVendorsWithBidSummaryAsync(request.Page, request.PageSize);
        var totalCount = await vendorRepository.GetTotalCountAsync();

        var vendorDtos = mapper.Map<IEnumerable<VendorDto>>(vendors);

        return new PagedResult<VendorDto>
        {
            Items = vendorDtos,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
} 