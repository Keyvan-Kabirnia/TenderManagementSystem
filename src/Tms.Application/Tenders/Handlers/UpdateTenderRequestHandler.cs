using AutoMapper;
using MediatR;
using Tms.Application.DTOs.Tender;
using Tms.Application.Tenders.Requests;
using Tms.Domain.Entities;
using Tms.Domain.Interfaces;

namespace Tms.Application.Tenders.Handlers;

public class UpdateTenderRequestHandler(
    ITenderRepository tenderRepository,
    ICategoryRepository categoryRepository,
    IStatusRepository statusRepository,
    IMapper mapper) 
    : IRequestHandler<UpdateTenderRequest, TenderDto>
{
    public async Task<TenderDto> Handle(UpdateTenderRequest request, CancellationToken cancellationToken)
    {
        var existingTender = await tenderRepository.GetByIdAsync(request.Id);
        if (existingTender is null)
        {
            throw new InvalidOperationException("Tender not found");
        }

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

        mapper.Map(request, existingTender);
        existingTender.UpdatedAt = DateTime.UtcNow;

        await tenderRepository.UpdateAsync(existingTender);

        return mapper.Map<TenderDto>(existingTender);
    }
}