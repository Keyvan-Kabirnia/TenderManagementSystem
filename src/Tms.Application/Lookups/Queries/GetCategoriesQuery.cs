using Tms.Application.Common;
using Tms.Application.DTOs.Lookup;

namespace Tms.Application.Lookups.Queries;

public record GetCategoriesQuery : IRequest<IEnumerable<CategoryDto>>; 