using Ecommerce.API.Data;
using Ecommerce.API.Features.Auth;
using Ecommerce.API.Features.Cart;
using Ecommerce.API.Settings;
using Ecommerce.API.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Tests.Cart;

public class CartServiceTests
{
    private readonly AppDbContext _db;
    private readonly CartService _cartService;
    private readonly AuthService _authService;

    public CartServiceTests()
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

        _cartService = new CartService(_db);
        _authService = new AuthService(_db, tokenService, jwtSettings);
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