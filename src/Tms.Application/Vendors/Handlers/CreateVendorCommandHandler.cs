using AutoMapper;
using MediatR;
using Tms.Application.DTOs.Vendor;
using Tms.Application.Vendors.Requests;
using Tms.Domain.Entities;
using Tms.Domain.Interfaces;

namespace Tms.Application.Vendors.Handlers;

public class CreateVendorCommandHandler : IRequestHandler<Requests.CreateVendorRequest, VendorDto>
{
    private readonly IVendorRepository _vendorRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateVendorCommandHandler(IVendorRepository vendorRepository, IUserRepository userRepository, IMapper mapper)
    {
        _vendorRepository = vendorRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<VendorDto> Handle(Requests.CreateVendorRequest request, CancellationToken cancellationToken)
    {
        // Validate user exists
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user is null)
        {
            throw new InvalidOperationException("User not found");
        }

        var vendor = _mapper.Map<VendorEntity>(request);
        vendor.CreatedAt = DateTime.UtcNow;

        await _vendorRepository.AddAsync(vendor);

        // Reload with related data for response
        var createdVendor = await _vendorRepository.GetVendorWithBidsAsync(vendor.Id);
        return _mapper.Map<VendorDto>(createdVendor);
    }
} 