using AutoMapper;
using MediatR;
using Tms.Application.DTOs.Lookup;
using Tms.Application.Lookups.Queries;
using Tms.Domain.Entities;
using Tms.Domain.Interfaces;

namespace Tms.Application.Lookups.Handlers;

public class GetStatusesQueryHandler : IRequestHandler<GetStatusesQuery, IEnumerable<StatusDto>>
{
    private readonly IStatusRepository _statusRepository;
    private readonly IMapper _mapper;

    public GetStatusesQueryHandler(IStatusRepository statusRepository, IMapper mapper)
    {
        _statusRepository = statusRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StatusDto>> Handle(GetStatusesQuery request, CancellationToken cancellationToken)
    {
        var statuses = await _statusRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<StatusDto>>(statuses);
    }
} 