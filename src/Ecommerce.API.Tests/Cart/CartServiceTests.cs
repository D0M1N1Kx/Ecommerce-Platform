using Ecommerce.API.Data;
using Ecommerce.API.Features.Cart;
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
}