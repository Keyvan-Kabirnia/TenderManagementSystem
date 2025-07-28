using Tms.Application.Common;
using Tms.Application.DTOs.Vendor;

namespace Tms.Application.Vendors.Queries;

public record GetVendorByIdQuery : IRequest<VendorDetailDto?>
{
    public int Id { get; init; }
} 