using Ecommerce.API.Data;
using Ecommerce.API.Features.Auth;
using Ecommerce.API.Features.Cart;
using Ecommerce.API.Models;
using Ecommerce.API.Settings;
using Ecommerce.API.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Tests.Cart;

public class CartServiceTests
{
    private readonly AppDbContext _db;
    private readonly CartService _cartService;

    public CartServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _db = new AppDbContext(options);

        _cartService = new CartService(_db);
    }

    private async Task<(User user, Models.Cart cart)> CreateTestUserWithCartAsync()
    {
        var user = new User { Username = "test", Email = "test@test.com", PasswordHash = "hash" };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        var cart = new Models.Cart { UserId = user.Id };
        _db.Carts.Add(cart);
        await _db.SaveChangesAsync();

        return (user, cart);
    }

    [Fact]
    public async Task GetAsync_ValidData_ReturnsUsersCart()
    {
        var (user, cartFromDb) = await CreateTestUserWithCartAsync();
        
        var resultCart = await _cartService.GetAsync(user.Id);
        
        Assert.NotNull(resultCart);
        Assert.NotNull(cartFromDb);
        Assert.Equal(user.Id, cartFromDb.UserId);
    }
}