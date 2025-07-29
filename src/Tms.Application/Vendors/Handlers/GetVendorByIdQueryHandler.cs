using AutoMapper;
using MediatR;
using Tms.Application.DTOs.Vendor;
using Tms.Application.Vendors.Queries;
using Tms.Domain.Interfaces;

namespace Tms.Application.Vendors.Handlers;

public class GetVendorByIdQueryHandler(IVendorRepository vendorRepository, IMapper mapper) 
    : IRequestHandler<GetVendorByIdQuery, VendorDetailDto?>
{
    public async Task<VendorDetailDto?> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
    {
        var vendor = await vendorRepository.GetVendorWithBidsAsync(request.Id);
        
        if (vendor is null)
        {
            return null;
        }

        return mapper.Map<VendorDetailDto>(vendor);
    }
} 