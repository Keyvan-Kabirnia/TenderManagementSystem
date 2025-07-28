using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tms.Application.DTOs.Common;
using Tms.Application.DTOs.Vendor;
using Tms.Application.Vendors.Requests;
using Tms.Application.Vendors.Queries;

namespace Tms.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public VendorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<VendorDto>>> GetVendors([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = new GetVendorsQuery
            {
                Page = page,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving vendors" });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VendorDetailDto>> GetVendor(int id)
    {
        try
        {
            var query = new GetVendorByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound(new { message = "Vendor not found" });
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving the vendor" });
        }
    }

    [HttpPost]
    public async Task<ActionResult<VendorDto>> CreateVendor([FromBody] CreateVendorRequest request)
    {
        try
        {
            var result = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetVendor), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch
        {
            return StatusCode(500, new { message = "An error occurred while creating the vendor" });
        }
    }
} 