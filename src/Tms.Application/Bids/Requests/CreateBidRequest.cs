using Tms.Application.Common;
using Tms.Application.DTOs.Bid;

namespace Tms.Application.Bids.Requests;

public record CreateBidRequest : IRequest<BidDto>
{
    public int TenderId { get; init; }
    public int VendorId { get; init; }
    public decimal Amount { get; init; }
    public string Comments { get; set; } = string.Empty;
}