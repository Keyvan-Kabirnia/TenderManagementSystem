using AutoMapper;
using MediatR;
using Tms.Application.DTOs.Bid;
using Tms.Application.Bids.Requests;
using Tms.Domain.Entities;
using Tms.Domain.Interfaces;

namespace Tms.Application.Bids.Handlers;

public class UpdateBidStatusCommandHandler(
    IBidRepository bidRepository,
    IStatusRepository statusRepository,
    IMapper mapper) : IRequestHandler<Requests.UpdateBidStatusRequest, BidDto>
{
    public async Task<BidDto> Handle(Requests.UpdateBidStatusRequest request, CancellationToken cancellationToken)
    {
        var bid = await bidRepository.GetByIdAsync(request.Id);

        if (bid is null)
        {
            throw new InvalidOperationException("Bid not found");
        }

        // Validate status exists and is a bid status
        var status = await statusRepository.GetByIdAsync(request.StatusId);
        if (status is null || status.Type != "Bid")
        {
            throw new InvalidOperationException("Invalid bid status");
        }

        bid.StatusId = request.StatusId;
        bid.UpdatedAt = DateTime.UtcNow;

        await bidRepository.UpdateAsync(bid);

        // Reload with related data for response
        var bids = await bidRepository.GetBidsByTenderAsync(bid.TenderId);
        var updatedBid = bids.FirstOrDefault(b => b.Id == bid.Id);
        
        return mapper.Map<BidDto>(updatedBid);
    }
} 