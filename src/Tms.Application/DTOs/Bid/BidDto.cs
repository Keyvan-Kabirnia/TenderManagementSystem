using Tms.Application.DTOs.Lookup;
using Tms.Application.DTOs.Vendor;

namespace Tms.Application.DTOs.Bid;

public class BidDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Comments { get; set; } = string.Empty;
    public DateTime SubmissionDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public VendorDto Vendor { get; set; } = null!;
    public StatusDto Status { get; set; } = null!;
} 