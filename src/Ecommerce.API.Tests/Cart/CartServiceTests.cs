using Ecommerce.API.Data;
using Ecommerce.API.Features.Cart;
using Ecommerce.API.Models;
using Ecommerce.Shared.DTOs.Cart.Requests;
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

    [Fact]
    public async Task AddItem_ValidItem_AddsItemToCart()
    {
        var (user, cart) = await CreateTestUserWithCartAsync();
        
        var category = new Models.Category
        {
            Name = "Groceries",
            Description = "Grocery items"
        };
        _db.Categories.Add(category);
        await _db.SaveChangesAsync();

        var product = new Product
        {
            Name = "Bread",
            Description = "White bread asdsadasd",
            Price = 5,
            Stock = 100,
            Sku = "ahjfgojsdjjgjdfsgjndkjfng",
            CategoryId = category.Id
        };
        _db.Products.Add(product);
        await _db.SaveChangesAsync();

        var request = new AddCartItemRequest
        {
            ProductId = product.Id,
            Quantity = 5
        };

        await _cartService.AddCartItemAsync(user.Id, request);

        var cartFromDb = await _db.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.Id == cart.Id);
        var addedItem = await _db.CartItems.FirstOrDefaultAsync(ci => ci.ProductId == product.Id);
        
        Assert.NotNull(cartFromDb);
        Assert.Contains(cartFromDb.CartItems, ci => ci.ProductId == product.Id);
        Assert.NotNull(addedItem);
        Assert.Equal(request.Quantity, addedItem.Quantity);
    }
}