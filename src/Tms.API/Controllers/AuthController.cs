using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tms.Application.Auth.Requests;
using Tms.Application.DTOs.Auth;

namespace Tms.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    IMediator mediator,
    IValidator<LoginRequest> loginRequestValidator,
    IValidator<RegisterRequest> registerRequestValidator)
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> LogIn([FromBody] LoginRequest request)
    {
        try
        {
            var validationResult = await loginRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await mediator.Send(request);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch
        {
            return StatusCode(500, new { message = "An error occurred during login" });
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult<LoginResponseDto>> Register([FromBody] RegisterRequest request)
    {
        try
        {
            var validationResult = await registerRequestValidator.ValidateAsync(request);
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
            return StatusCode(500, new { message = "An error occurred during registration" });
        }
    }
}