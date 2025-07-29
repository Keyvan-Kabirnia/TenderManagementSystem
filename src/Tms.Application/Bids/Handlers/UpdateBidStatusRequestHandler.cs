using AutoMapper;
using MediatR;
using Tms.Application.DTOs.Bid;
using Tms.Application.Bids.Requests;
using Tms.Domain.Entities;
using Tms.Domain.Interfaces;

namespace Tms.Application.Bids.Handlers;

public class UpdateBidStatusRequestHandler(
    IBidRepository bidRepository,
    IStatusRepository statusRepository,
    IMapper mapper) 
    : IRequestHandler<UpdateBidStatusRequest, BidDto>
{
    public async Task<BidDto> Handle(UpdateBidStatusRequest request, CancellationToken cancellationToken)
    {
        var existingBid = await bidRepository.GetByIdAsync(request.Id);
        if (existingBid is null)
        {
            throw new InvalidOperationException("Bid not found");
        }

        var status = await statusRepository.GetByIdAsync(request.StatusId);
        if (status is null || status.Type != "Bid")
        {
            throw new InvalidOperationException("Invalid bid status");
        }

        existingBid.StatusId = request.StatusId;
        existingBid.UpdatedAt = DateTime.UtcNow;

        await bidRepository.UpdateAsync(existingBid);
        
        return mapper.Map<BidDto>(existingBid);
    }
} 