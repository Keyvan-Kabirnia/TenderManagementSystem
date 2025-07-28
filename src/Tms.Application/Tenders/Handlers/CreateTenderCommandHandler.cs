using AutoMapper;
using MediatR;
using Tms.Application.DTOs.Tender;
using Tms.Application.Tenders.Requests;
using Tms.Domain.Entities;
using Tms.Domain.Interfaces;

namespace Tms.Application.Tenders.Handlers;

public class CreateTenderCommandHandler : IRequestHandler<Requests.CreateTenderRequest, TenderDto>
{
    private readonly ITenderRepository _tenderRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IStatusRepository _statusRepository;
    private readonly IMapper _mapper;

    public CreateTenderCommandHandler(
        ITenderRepository tenderRepository,
        ICategoryRepository  categoryRepository,
        IStatusRepository statusRepository,
        IMapper mapper)
    {
        _tenderRepository = tenderRepository;
        _categoryRepository = categoryRepository;
        _statusRepository = statusRepository;
        _mapper = mapper;
    }

    public async Task<TenderDto> Handle(Requests.CreateTenderRequest request, CancellationToken cancellationToken)
    {
        // Validate category exists
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (category is null)
        {
            throw new InvalidOperationException("Category not found");
        }

        // Validate status exists
        var status = await _statusRepository.GetByIdAsync(request.StatusId);
        if (status is null)
        {
            throw new InvalidOperationException("Status not found");
        }

        var tender = _mapper.Map<TenderEntity>(request);
        tender.CreatedAt = DateTime.UtcNow;

        await _tenderRepository.AddAsync(tender);

        // Reload with related data for response
        var createdTender = await _tenderRepository.GetTenderWithDetailsAsync(tender.Id);
        return _mapper.Map<TenderDto>(createdTender);
    }
} 