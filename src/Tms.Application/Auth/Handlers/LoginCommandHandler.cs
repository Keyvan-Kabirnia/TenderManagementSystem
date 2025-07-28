using BCrypt.Net;
using MediatR;
using Tms.Application.Auth.Requests;
using Tms.Application.DTOs.Auth;
using Tms.Domain.Interfaces;
using Tms.Infrastructure.Services;

namespace Tms.Application.Auth.Handlers;

public class LoginCommandHandler : IRequestHandler<Requests.LoginRequest, LoginResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<LoginResponseDto> Handle(Requests.LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUserNameAsync(request.UserName);
        
        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid userName or password");
        }

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