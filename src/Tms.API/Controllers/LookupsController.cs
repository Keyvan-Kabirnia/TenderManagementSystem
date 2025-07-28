using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tms.Application.DTOs.Lookup;
using Tms.Application.Lookups.Queries;

namespace Tms.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LookupsController(IMediator mediator) : ControllerBase
{
    [HttpGet("categories")]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
        try
        {
            var query = new GetCategoriesQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving categories" });
        }
    }

    [HttpGet("statuses")]
    public async Task<ActionResult<IEnumerable<StatusDto>>> GetStatuses()
    {
        try
        {
            var query = new GetStatusesQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving statuses" });
        }
    }
} 