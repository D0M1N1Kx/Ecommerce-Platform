using System.Data;
using Ecommerce.API.Data;
using Ecommerce.API.Models;
using Ecommerce.API.Settings;
using Ecommerce.API.Shared.Services;
using Ecommerce.Shared.DTOs.Auth.Responses;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Features.Auth;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly ITokenService _tokenService;
    private readonly JwtSettings _jwtSettings;

    public AuthService(AppDbContext db, ITokenService tokenService, JwtSettings jwtSettings)
    {
        _db = db;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings;
    }
    
    public async Task RegisterAsync(string username, string email, string password)
    {
        var existingUser = await _db.Users.AnyAsync(x => x.Username == username || x.Email == email);
        if (existingUser)
            throw new DuplicateNameException($"Username or email is already registered.");
        
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        var user = new User
        {
            Username = username,
            Email = email,
            PasswordHash = hashedPassword
        };
        
        _db.Users.Add(user);

        var cart = new Models.Cart
        {
            UserId = user.Id,
        };
        
        _db.Carts.Add(cart);
        
        await _db.SaveChangesAsync();
    }

    public async Task<LoginResponse> LoginAsync(string email, string password)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email)
            ?? throw new KeyNotFoundException("Invalid email or password.");

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            throw new KeyNotFoundException("Invalid username or password.");
        
        var token = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        var newRefreshToken = new RefreshToken
        {
            UserId = user.Id,
            TokenHash = _tokenService.HashToken(refreshToken),
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays)
        };

        _db.RefreshTokens.Add(newRefreshToken);
        await _db.SaveChangesAsync();
        
        return new LoginResponse { AccessToken = token, RefreshToken = refreshToken };
    }

    public async Task<RefreshResponse> RefreshAsync(string refreshToken)
    {
        var hashedToken = _tokenService.HashToken(refreshToken);

        var storedToken = await _db.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.TokenHash == hashedToken);

        if (storedToken == null)
            throw new UnauthorizedAccessException("Invalid refresh token");

        if (storedToken.Revoked)
            throw new UnauthorizedAccessException("Refresh token has been revoked");

        if (storedToken.ExpiresAt < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Refresh token has expired");

        var newAccessToken = _tokenService.GenerateAccessToken(storedToken.User);

        return new RefreshResponse { AccessToken = newAccessToken };
    }
}