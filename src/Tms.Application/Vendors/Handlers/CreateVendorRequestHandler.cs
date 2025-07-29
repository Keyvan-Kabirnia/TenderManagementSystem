using AutoMapper;
using MediatR;
using Tms.Application.DTOs.Vendor;
using Tms.Application.Vendors.Requests;
using Tms.Domain.Entities;
using Tms.Domain.Interfaces;

namespace Tms.Application.Vendors.Handlers;

public class CreateVendorRequestHandler(
    IVendorRepository vendorRepository,
    IUserRepository userRepository,
    IMapper mapper) 
    : IRequestHandler<CreateVendorRequest, VendorDto>
{
    public async Task<VendorDto> Handle(CreateVendorRequest request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);
        if (user is null)
        {
            throw new InvalidOperationException("User not found");
        }

        var vendor = mapper.Map<VendorEntity>(request);
        vendor.CreatedAt = DateTime.UtcNow;

        var createdVendor = await vendorRepository.AddAsync(vendor);

        return mapper.Map<VendorDto>(createdVendor);
    }
} 