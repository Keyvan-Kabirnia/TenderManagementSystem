using AutoMapper;
using MediatR;
using Tms.Application.DTOs.Tender;
using Tms.Application.Tenders.Queries;
using Tms.Domain.Interfaces;

namespace Tms.Application.Tenders.Handlers;

public class GetTenderByIdQueryHandler(ITenderRepository tenderRepository, IMapper mapper) 
    : IRequestHandler<GetTenderByIdQuery, TenderDetailDto?>
{
    public async Task<TenderDetailDto?> Handle(GetTenderByIdQuery request, CancellationToken cancellationToken)
    {
        var tender = await tenderRepository.GetTenderWithDetailsAsync(request.Id);
        
        if (tender is null)
        {
            return null;
        }

        return mapper.Map<TenderDetailDto>(tender);
    }
} 