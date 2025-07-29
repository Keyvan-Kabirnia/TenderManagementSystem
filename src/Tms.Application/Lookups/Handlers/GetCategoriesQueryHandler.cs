using AutoMapper;
using MediatR;
using Tms.Application.DTOs.Lookup;
using Tms.Application.Lookups.Queries;
using Tms.Domain.Entities;
using Tms.Domain.Interfaces;

namespace Tms.Application.Lookups.Handlers;

public class GetCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper) : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryDto>>
{
    public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAllAsync();
        return mapper.Map<IEnumerable<CategoryDto>>(categories);
    }
}