using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tms.Application.DTOs.Common;
using Tms.Application.DTOs.Vendor;
using Tms.Application.Vendors.Requests;
using Tms.Application.Vendors.Queries;
using FluentValidation;
using Tms.Application.Vendors.Validators;
using Tms.Application.Auth.Validators;

namespace Tms.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendorsController(IMediator mediator, IValidator<CreateVendorRequest> createVendorRequestValidator) : ControllerBase
{
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

            var result = await mediator.Send(query);
            return Ok(result);
        }
        catch
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
            var result = await mediator.Send(query);

            if (result == null)
            {
                return NotFound(new { message = "Vendor not found" });
            }

            return Ok(result);
        }
        catch
        {
            return StatusCode(500, new { message = "An error occurred while retrieving the vendor" });
        }
    }

    [HttpPost]
    public async Task<ActionResult<VendorDto>> CreateVendor([FromBody] CreateVendorRequest request)
    {
        try
        {
            var validationResult = await createVendorRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await mediator.Send(request);
            return Ok(result);
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