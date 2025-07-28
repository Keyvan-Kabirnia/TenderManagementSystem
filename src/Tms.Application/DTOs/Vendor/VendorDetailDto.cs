using Tms.Application.DTOs.Lookup;

namespace Tms.Application.DTOs.Vendor;

public class VendorDetailDto : VendorDto
{
    public IEnumerable<VendorBidDto> Bids { get; set; } = [];
}

public class VendorBidDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime SubmissionDate { get; set; }
    public string TenderTitle { get; set; } = string.Empty;
    public StatusDto Status { get; set; } = null!;
}