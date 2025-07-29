using AutoMapper;
using MediatR;
using Tms.Application.DTOs.Lookup;
using Tms.Application.Lookups.Queries;
using Tms.Domain.Entities;
using Tms.Domain.Interfaces;

namespace Tms.Application.Lookups.Handlers;

public class GetStatusesQueryHandler(IStatusRepository statusRepository, IMapper mapper) : IRequestHandler<GetStatusesQuery, IEnumerable<StatusDto>>
{
    public async Task<IEnumerable<StatusDto>> Handle(GetStatusesQuery request, CancellationToken cancellationToken)
    {
        var statuses = await statusRepository.GetAllAsync();
        return mapper.Map<IEnumerable<StatusDto>>(statuses);
    }
} 