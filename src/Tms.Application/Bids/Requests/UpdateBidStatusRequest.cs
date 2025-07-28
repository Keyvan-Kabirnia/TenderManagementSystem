using Tms.Application.Common;
using Tms.Application.DTOs.Bid;

namespace Tms.Application.Bids.Requests;

public record UpdateBidStatusRequest : IRequest<BidDto>
{
    public int Id { get; init; }
    public int StatusId { get; init; }
} 