using AutoMapper;
using Tms.Application.DTOs.Bid;
using Tms.Application.Bids.Requests;
using Tms.Domain.Entities;
using Tms.Domain.Interfaces;
using Tms.Application.Common;

namespace Tms.Application.Bids.Handlers;

public class CreateBidCommandHandler : IRequestHandler<Requests.CreateBidRequest, BidDto>
{
    private readonly IBidRepository _bidRepository;
    private readonly ITenderRepository _tenderRepository;
    private readonly IVendorRepository _vendorRepository;
    private readonly IStatusRepository _statusRepository;
    private readonly IMapper _mapper;

    public CreateBidCommandHandler(
        IBidRepository bidRepository,
        ITenderRepository tenderRepository,
        IVendorRepository vendorRepository,
        IStatusRepository statusRepository,
        IMapper mapper)
    {
        _bidRepository = bidRepository;
        _tenderRepository = tenderRepository;
        _vendorRepository = vendorRepository;
        _statusRepository = statusRepository;
        _mapper = mapper;
    }

    public async Task<BidDto> Handle(Requests.CreateBidRequest request, CancellationToken cancellationToken)
    {
        // Validate tender exists and is active
        var tender = await _tenderRepository.GetByIdAsync(request.TenderId);
        if (tender is null)
        {
            throw new InvalidOperationException("Tender not found");
        }

        // Validate vendor exists and is active
        var vendor = await _vendorRepository.GetByIdAsync(request.VendorId);
        if (vendor is null )
        {
            throw new InvalidOperationException("Vendor not found");
        }

        // Check if vendor already has a bid on this tender
        if (await _bidRepository.VendorHasBidOnTenderAsync(request.VendorId, request.TenderId))
        {
            throw new InvalidOperationException("Vendor already has a bid on this tender");
        }

        // Get pending status for bids
        var pendingStatus = (await _statusRepository.GetAllAsync())
            .Cast<StatusEntity>()
            .FirstOrDefault(s => s.Type == "Bid" && s.Name == "Pending");

        if (pendingStatus is null)
        {
            throw new InvalidOperationException("Pending status not found");
        }

        var bid = _mapper.Map<BidEntity>(request);
        bid.StatusId = pendingStatus.Id;
        bid.SubmissionDate = DateTime.UtcNow;
        bid.CreatedAt = DateTime.UtcNow;

        var createdBid = await _bidRepository.AddAsync(bid);
        
        return _mapper.Map<BidDto>(createdBid);
    }
} 