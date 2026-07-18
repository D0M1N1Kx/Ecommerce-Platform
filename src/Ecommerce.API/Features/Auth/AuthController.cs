using Ecommerce.Shared.DTOs.Auth.Requests;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = Ecommerce.Shared.DTOs.Auth.Requests.LoginRequest;
using RegisterRequest = Ecommerce.Shared.DTOs.Auth.Requests.RegisterRequest;

namespace Ecommerce.API.Features.Auth;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        await _authService.RegisterAsync(request.Username, request.Email, request.Password);

        return Ok(new { message = "Registration successful." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginRequest request)
    {
        var login = await _authService.LoginAsync(request.Email, request.Password);
        
        return Ok(login);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest request)
    {
        var newAccessToken = await _authService.RefreshAsync(request.RefreshToken);

        return Ok(newAccessToken);
    }
}