using Ecommerce.API.Data;
using Ecommerce.API.Features.Auth;
using Ecommerce.API.Settings;
using Ecommerce.API.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Tests.Auth;

public class AuthServiceTests
{
    private readonly AppDbContext _db;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _db = new AppDbContext(options);

        var jwtSettings = new JwtSettings
        {
            SecretKey = "HE6gOs3hvkrjApAxvQxC3IBawseJEhPDUWWvRyyihD0=",
            Issuer = "EcommerceAPI",
            Audience = "EcommerceClient",
            AccessTokenExpirationMinutes = 15,
            RefreshTokenExpirationDays = 7
        };

        var tokenService = new TokenService(jwtSettings);

        _authService = new AuthService(_db, tokenService, jwtSettings);
    }

    [Fact]
    public async Task RegisterAndLogin_WithValidData_SucceedsAndReturnTokens()
    {
        var username = "testuser";
        var email = "test@ecommerce.com";
        var password = "VeryStrongPassword";

        await _authService.RegisterAsync(username, email, password);

        var userInDb = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        Assert.NotNull(userInDb);
        Assert.Equal(username, userInDb.Username);

        var loginResponse = await _authService.LoginAsync(email, password);

        Assert.NotNull(loginResponse);
        Assert.False(string.IsNullOrEmpty(loginResponse.AccessToken));
        Assert.False(string.IsNullOrEmpty(loginResponse.RefreshToken));

        var refreshTokenInDb = await _db.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == userInDb.Id);
        Assert.NotNull(refreshTokenInDb);
    }

    [Fact]
    public async Task Refresh_WithValidToken_ReturnsNewAccessToken()
    {
        var username = "refreshUser";
        var email = "refresh@ecommerce.com";
        var password = "VeryStrongPassword";

        await _authService.RegisterAsync(username, email, password);

        var loginResponse = await _authService.LoginAsync(email, password);
        var validRefreshToken = loginResponse.RefreshToken;

        var refreshResponse = await _authService.RefreshAsync(validRefreshToken);

        Assert.NotNull(refreshResponse);
        Assert.False(string.IsNullOrEmpty(refreshResponse.AccessToken));
        
        Assert.NotEqual(loginResponse.AccessToken, refreshResponse.AccessToken);
    }
    
    [Fact]
    public async Task RegisterAsync_ValidData_CreatesEmptyCartForUser()
    {
        var username = "testuser";
        var email = "testuser@ecommerce.com";
        var password = "VeryStrongPassword";

        await _authService.RegisterAsync(username, email, password);

        var userInDb = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        Assert.NotNull(userInDb);
        
        var cart = await _db.Carts.FirstOrDefaultAsync(c => c.UserId == userInDb.Id);

        Assert.NotNull(cart);
        Assert.Equal(cart.UserId, userInDb.Id);
    }
}