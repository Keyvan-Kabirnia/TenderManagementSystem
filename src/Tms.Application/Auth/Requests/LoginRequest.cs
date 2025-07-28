using Tms.Application.Common;
using Tms.Application.DTOs.Auth;

namespace Tms.Application.Auth.Requests;

public record LoginRequest : IRequest<LoginResponseDto>
{
    public string UserName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
} 