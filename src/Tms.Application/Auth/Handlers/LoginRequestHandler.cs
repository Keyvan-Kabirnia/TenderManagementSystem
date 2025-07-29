using BCrypt.Net;
using MediatR;
using Microsoft.Extensions.Options;
using Tms.Application.Auth.Requests;
using Tms.Application.DTOs.Auth;
using Tms.Domain.Configurations;
using Tms.Domain.Interfaces;
using Tms.Infrastructure.Services;

namespace Tms.Application.Auth.Handlers;

public class LoginRequestHandler(
    IUserRepository userRepository,
    IJwtService jwtService,
    IOptions<JWTConfiguration> jwtConfiguration) 
    : IRequestHandler<LoginRequest, LoginResponseDto>
{
    private readonly JWTConfiguration jwtConfiguration = jwtConfiguration.Value;

    public async Task<LoginResponseDto> Handle(Requests.LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByUserNameAsync(request.UserName);
        
        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid userName or password");
        }

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