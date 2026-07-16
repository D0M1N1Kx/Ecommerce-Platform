using Ecommerce.API.Data;
using Ecommerce.API.Mappings;
using Ecommerce.API.Models;
using Ecommerce.Shared.DTOs.Cart.Requests;
using Ecommerce.Shared.DTOs.Cart.Responses;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Features.Cart;

public class CartService : ICartService
{
    private readonly AppDbContext _db;

    public CartService(AppDbContext db)
    {
        _db = db;
    }
    
    public async Task<CartResponse> GetAsync(Guid userId)
    {
        var cart = await GetCartFromUserId(userId)
            ?? throw new KeyNotFoundException("Cart not found");

        return cart.MapToResponse()
            ?? throw new InvalidOperationException("Failed to map cart to response");
    }

    public async Task AddCartItemAsync(Guid userId, AddCartItemRequest request)
    {
        var cart = await GetCartFromUserId(userId)
            ?? throw new KeyNotFoundException("Cart not found");

        var productExists = await _db.Products
            .AnyAsync(x => x.Id == request.ProductId);
        if (!productExists)
            throw new KeyNotFoundException("Product not found");

        var existingItem = await _db.CartItems
            .FirstOrDefaultAsync(x => x.CartId == cart.Id && x.ProductId == request.ProductId);

        if (existingItem != null)
        {
            existingItem.Quantity += request.Quantity;
        }
        else
        {
            var item = new CartItem
            {
                CartId = cart.Id,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };

            _db.CartItems.Add(item);
        }

        await _db.SaveChangesAsync();
    }

    public async Task ChangeQuantityAsync(Guid userId, ChangeCartItemQuantityRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteCartItemAsync(Guid userId, Guid productId)
    {
        throw new NotImplementedException();
    }

    public async Task ClearCartAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    private async Task<Models.Cart?> GetCartFromUserId(Guid userId)
    {
        var cart = await _db.Carts
            .AsNoTracking()
            .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.Category)
            .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Discount)
            .FirstOrDefaultAsync(x => x.UserId == userId);

        return cart;
    }

    private async Task<List<CartItem>> GetCartItemsFromCartId(Guid cartId)
    {
        var cartItems = await _db.CartItems
            .Where(x => x.CartId == cartId)
            .ToListAsync();

        return cartItems ?? [];
    }
}