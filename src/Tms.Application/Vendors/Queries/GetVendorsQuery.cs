using Tms.Application.Common;
using Tms.Application.DTOs.Common;
using Tms.Application.DTOs.Vendor;

namespace Tms.Application.Vendors.Queries;

public record GetVendorsQuery : IRequest<PagedResult<VendorDto>>
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
} 