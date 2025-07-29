using Microsoft.Extensions.Options;
using Tms.Application.Auth.Requests;
using Tms.Application.Common;
using Tms.Application.DTOs.Auth;
using Tms.Domain.Configurations;
using Tms.Domain.Entities;
using Tms.Domain.Interfaces;
using Tms.Infrastructure.Services;

namespace Tms.Application.Auth.Handlers;

public class RegisterRequestHandler(
    IUserRepository userRepository,
    IJwtService jwtService,
    IOptions<JWTConfiguration> jwtConfiguration)
    : IRequestHandler<RegisterRequest, LoginResponseDto>
{
    private readonly JWTConfiguration jwtConfiguration = jwtConfiguration.Value;

    public async Task<LoginResponseDto> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        if (await userRepository.UserNameExistsAsync(request.UserName))
        {
            throw new InvalidOperationException("UserName already exists");
        }

        if (!string.IsNullOrEmpty(request.Email) && await userRepository.EmailExistsAsync(request.Email))
        {
            throw new InvalidOperationException("Email already exists");
        }

        var user = new UserEntity
        {
            UserName = request.UserName,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = request.Role,
            CreatedAt = DateTime.UtcNow
        };

        await userRepository.AddAsync(user);

        // Generate token and return response
        var token = jwtService.GenerateToken(user);
        var expiresAt = DateTime.UtcNow.AddHours(jwtConfiguration.ExpirationHours);

        return new LoginResponseDto
        {
            Token = token,
            UserName = user.UserName,
            Role = user.Role,
            ExpiresAt = expiresAt
        };
    }
}