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
            SecretKey = "",
            Issuer = "EcommerceAPI",
            Audience = "EcommerceClient",
            AccessTokenExpirationMinutes = 15,
            RefreshTokenExpirationDays = 7
        };

        var tokenService = new TokenService(jwtSettings);

        _authService = new AuthService(_db, tokenService, jwtSettings);
    }
}