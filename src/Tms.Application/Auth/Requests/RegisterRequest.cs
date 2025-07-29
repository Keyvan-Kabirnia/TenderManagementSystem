using Tms.Application.Common;
using Tms.Application.DTOs.Auth;
using Tms.Domain.Enums;

namespace Tms.Application.Auth.Requests;

public record RegisterRequest : IRequest<LoginResponseDto>
{
    public string UserName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public UserRole Role { get; init; }
} 