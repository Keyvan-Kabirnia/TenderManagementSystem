using AutoMapper;
using MediatR;
using Tms.Application.DTOs.Vendor;
using Tms.Application.Vendors.Queries;
using Tms.Domain.Interfaces;

namespace Tms.Application.Vendors.Handlers;

public class GetVendorByIdQueryHandler : IRequestHandler<GetVendorByIdQuery, VendorDetailDto?>
{
    private readonly IVendorRepository _vendorRepository;
    private readonly IMapper _mapper;

    public GetVendorByIdQueryHandler(IVendorRepository vendorRepository, IMapper mapper)
    {
        _vendorRepository = vendorRepository;
        _mapper = mapper;
    }

    public async Task<VendorDetailDto?> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
    {
        var vendor = await _vendorRepository.GetVendorWithBidsAsync(request.Id);
        
        if (vendor is null)
        {
            return null;
        }

        return _mapper.Map<VendorDetailDto>(vendor);
    }
} 