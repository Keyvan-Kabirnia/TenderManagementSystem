using AutoMapper;
using MediatR;
using Tms.Application.DTOs.Tender;
using Tms.Application.Tenders.Requests;
using Tms.Domain.Entities;
using Tms.Domain.Interfaces;

namespace Tms.Application.Tenders.Handlers;

public class CreateTenderRequestHandler(
    ITenderRepository tenderRepository,
    ICategoryRepository categoryRepository,
    IStatusRepository statusRepository,
    IMapper mapper) 
    : IRequestHandler<CreateTenderRequest, TenderDto>
{
    public async Task<TenderDto> Handle(CreateTenderRequest request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.CategoryId);
        if (category is null)
        {
            throw new InvalidOperationException("Category not found");
        }

        var status = await statusRepository.GetByIdAsync(request.StatusId);
        if (status is null)
        {
            throw new InvalidOperationException("Status not found");
        }

        var tender = mapper.Map<TenderEntity>(request);
        tender.CreatedAt = DateTime.UtcNow;

        await tenderRepository.AddAsync(tender);

        var createdTender = await tenderRepository.GetTenderWithDetailsAsync(tender.Id);
        return mapper.Map<TenderDto>(createdTender);
    }
} 