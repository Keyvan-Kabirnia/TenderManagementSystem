using Tms.Application.Auth.Requests;
using Tms.Application.Common;
using Tms.Application.DTOs.Auth;
using Tms.Domain.Entities;
using Tms.Domain.Interfaces;
using Tms.Infrastructure.Services;

namespace Tms.Application.Auth.Handlers;

public class RegisterCommandHandler : IRequestHandler<RegisterRequest, LoginResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public RegisterCommandHandler(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<LoginResponseDto> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        // Check if userName already exists
        if (await _userRepository.UserNameExistsAsync(request.UserName))
        {
            throw new InvalidOperationException("UserName already exists");
        }

        // Check if email already exists
        if (await _userRepository.EmailExistsAsync(request.Email))
        {
            throw new InvalidOperationException("Email already exists");
        }

        // Create new user
        var user = new UserEntity
        {
            UserName = request.UserName,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = request.Role,
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);

        // Generate token and return response
        var token = _jwtService.GenerateToken(user);
        var expiresAt = DateTime.UtcNow.AddHours(24); // This should come from configuration

        return new LoginResponseDto
        {
            Token = token,
            UserName = user.UserName,
            Role = user.Role,
            ExpiresAt = expiresAt
        };
    }
}