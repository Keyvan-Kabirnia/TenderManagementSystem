using AutoMapper;
using Tms.Application.DTOs.Bid;
using Tms.Application.Bids.Requests;
using Tms.Domain.Entities;
using Tms.Domain.Interfaces;
using Tms.Application.Common;

namespace Tms.Application.Bids.Handlers;

public class CreateBidRequestHandler(
    IBidRepository bidRepository,
    ITenderRepository tenderRepository,
    IVendorRepository vendorRepository,
    IStatusRepository statusRepository,
    IMapper mapper) 
    : IRequestHandler<Requests.CreateBidRequest, BidDto>
{
    public async Task<BidDto> Handle(Requests.CreateBidRequest request, CancellationToken cancellationToken)
    {
        var tender = await tenderRepository.GetByIdAsync(request.TenderId);
        if (tender is null)
        {
            throw new InvalidOperationException("Tender not found");
        }

        var vendor = await vendorRepository.GetByIdAsync(request.VendorId);
        if (vendor is null )
        {
            throw new InvalidOperationException("Vendor not found");
        }

        if (await bidRepository.VendorHasBidOnTenderAsync(request.VendorId, request.TenderId))
        {
            throw new InvalidOperationException("Vendor already has a bid on this tender");
        }

        // Get pending status for bids
        var pendingStatus = (await statusRepository.GetAllAsync())
            .Cast<StatusEntity>()
            .FirstOrDefault(s => s.Type == "Bid" && s.Name == "Pending");

        if (pendingStatus is null)
        {
            throw new InvalidOperationException("Pending status not found");
        }

        var bid = mapper.Map<BidEntity>(request);
        bid.StatusId = pendingStatus.Id;
        bid.SubmissionDate = DateTime.UtcNow;
        bid.CreatedAt = DateTime.UtcNow;

        var createdBid = await bidRepository.AddAsync(bid);
        
        return mapper.Map<BidDto>(createdBid);
    }
} 