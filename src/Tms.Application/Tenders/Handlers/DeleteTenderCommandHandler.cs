using MediatR;
using Tms.Application.Tenders.Requests;
using Tms.Domain.Interfaces;

namespace Tms.Application.Tenders.Handlers;

public class DeleteTenderCommandHandler : IRequestHandler<DeleteTenderRequest, bool>
{
    private readonly ITenderRepository _tenderRepository;

    public DeleteTenderCommandHandler(ITenderRepository tenderRepository)
    {
        _tenderRepository = tenderRepository;
    }

    public async Task<bool> Handle(DeleteTenderRequest request, CancellationToken cancellationToken)
    {
        var tender = await _tenderRepository.GetByIdAsync(request.Id);
        if (tender is null)
        {
            throw new InvalidOperationException("Tender not found");
        }

        await _tenderRepository.DeleteAsync(tender);
        return true;
    }
} 