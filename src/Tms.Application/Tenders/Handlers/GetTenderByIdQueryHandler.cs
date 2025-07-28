using AutoMapper;
using MediatR;
using Tms.Application.DTOs.Tender;
using Tms.Application.Tenders.Queries;
using Tms.Domain.Interfaces;

namespace Tms.Application.Tenders.Handlers;

public class GetTenderByIdQueryHandler : IRequestHandler<GetTenderByIdQuery, TenderDetailDto?>
{
    private readonly ITenderRepository _tenderRepository;
    private readonly IMapper _mapper;

    public GetTenderByIdQueryHandler(ITenderRepository tenderRepository, IMapper mapper)
    {
        _tenderRepository = tenderRepository;
        _mapper = mapper;
    }

    public async Task<TenderDetailDto?> Handle(GetTenderByIdQuery request, CancellationToken cancellationToken)
    {
        var tender = await _tenderRepository.GetTenderWithDetailsAsync(request.Id);
        
        if (tender is null)
        {
            return null;
        }

        return _mapper.Map<TenderDetailDto>(tender);
    }
} 