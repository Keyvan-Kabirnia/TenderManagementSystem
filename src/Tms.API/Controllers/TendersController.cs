using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tms.Application.DTOs.Common;
using Tms.Application.DTOs.Tender;
using Tms.Application.Tenders.Requests;
using Tms.Application.Tenders.Queries;

namespace Tms.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TendersController : ControllerBase
{
    private readonly IMediator _mediator;

    public TendersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<TenderDto>>> GetTenders([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = new GetTendersQuery
            {
                Page = page,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving tenders" });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TenderDetailDto>> GetTender(int id)
    {
        try
        {
            var query = new GetTenderByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound(new { message = "Tender not found" });
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving the tender" });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<TenderDto>> CreateTender([FromBody] CreateTenderRequest request)
    {
        try
        {
            var result = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetTender), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating the tender" });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<TenderDto>> UpdateTender(int id, [FromBody] UpdateTenderRequest request)
    {
        try
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the tender" });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteTender(int id)
    {
        try
        {
            var command = new DeleteTenderRequest { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch
        {
            return StatusCode(500, new { message = "An error occurred while deleting the tender" });
        }
    }
} 