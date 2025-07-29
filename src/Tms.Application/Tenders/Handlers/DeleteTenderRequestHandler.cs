using MediatR;
using Tms.Application.Tenders.Requests;
using Tms.Domain.Interfaces;

namespace Tms.Application.Tenders.Handlers;

public class DeleteTenderRequestHandler(ITenderRepository tenderRepository) 
    : IRequestHandler<DeleteTenderRequest, bool>
{
    public async Task<bool> Handle(DeleteTenderRequest request, CancellationToken cancellationToken)
    {
        var tender = await tenderRepository.GetByIdAsync(request.Id);
        if (tender is null)
        {
            throw new InvalidOperationException("Tender not found");
        }

        await tenderRepository.DeleteAsync(tender);
        return true;
    }
} 