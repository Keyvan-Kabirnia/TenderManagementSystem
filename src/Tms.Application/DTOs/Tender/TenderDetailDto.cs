using Tms.Application.DTOs.Bid;

namespace Tms.Application.DTOs.Tender;

public class TenderDetailDto : TenderDto
{
    public IEnumerable<BidDto> Bids { get; set; } = [];
} 