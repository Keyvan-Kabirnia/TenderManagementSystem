using Tms.Application.Common;
using Tms.Application.DTOs.Vendor;

namespace Tms.Application.Vendors.Requests;

public record CreateVendorRequest : IRequest<VendorDto>
{
    public string Name { get; init; } = string.Empty;
    public string ContactPerson { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public string TaxNumber { get; init; } = string.Empty;
    public int UserId { get; init; }
} 