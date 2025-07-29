using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tms.Application.DTOs.Bid;
using Tms.Application.Bids.Requests;
using Tms.Application.Auth.Validators;
using Tms.Application.Bids.Validators;
using FluentValidation;

namespace Tms.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BidsController(
    IMediator mediator,
    IValidator<CreateBidRequest> createBidRequestValidator) 
    : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<BidDto>> CreateBid([FromBody] CreateBidRequest request)
    {
        try
        {
            var validationResult = await createBidRequestValidator.ValidateAsync(request);
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
            return StatusCode(500, new { message = "An error occurred while creating the bid" });
        }
    }

    [HttpPut("{id}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BidDto>> UpdateBidStatus(int id, [FromBody] UpdateBidStatusRequest request)
    {
        try
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch
        {
            return StatusCode(500, new { message = "An error occurred while updating the bid status" });
        }
    }
}