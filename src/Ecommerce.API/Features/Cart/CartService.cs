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
        var cart = await GetCartFromUserId(userId, true)
            ?? throw new KeyNotFoundException("Cart not found");

        var product = await _db.Products
                          .FirstOrDefaultAsync(x => x.Id == request.ProductId)
                      ?? throw new KeyNotFoundException("Product not found");

        var existingItem = cart.CartItems
            .FirstOrDefault(x => x.ProductId == request.ProductId);

        if (existingItem != null)
        {
            if (product.Stock < existingItem.Quantity + request.Quantity)
                throw new InvalidOperationException($"Not enough stock. Only {product.Stock} items available");
            
            existingItem.Quantity += request.Quantity;
        }
        else
        {
            if (product.Stock < request.Quantity)
                throw new InvalidOperationException($"Not enough stock. Only {product.Stock} items available");
            
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
        var cart = await GetCartFromUserId(userId, true)
                   ?? throw new KeyNotFoundException("Cart not found");

        var existingItem = cart.CartItems
            .FirstOrDefault(x => x.ProductId == request.ProductId)
            ?? throw new KeyNotFoundException("Existing item not found");

        if (request.IsIncrement)
        {
            var targetQuantity = existingItem.Quantity + request.Amount;

            if (existingItem.Product.Stock < targetQuantity)
                throw new InvalidOperationException($"Not enough stock. Only {existingItem.Product.Stock} items available");

            existingItem.Quantity = targetQuantity;
        }
        else
        {
            existingItem.Quantity -= request.Amount;
            if (existingItem.Quantity <= 0)
            {
                _db.CartItems.Remove(existingItem);
            }
        }

        await _db.SaveChangesAsync();
    }

    public async Task DeleteCartItemAsync(Guid userId, Guid productId)
    {
        var cart = await GetCartFromUserId(userId, true)
                   ?? throw new KeyNotFoundException("Cart not found");

        var existingItem = cart.CartItems
              .FirstOrDefault(x => x.ProductId == productId) 
              ?? throw new KeyNotFoundException("Existing item not found");

        _db.CartItems.Remove(existingItem);
        await _db.SaveChangesAsync();
    }

    public async Task ClearCartAsync(Guid userId)
    {
        var cart = await GetCartFromUserId(userId, true)
                   ?? throw new KeyNotFoundException("Cart not found");

        if (!cart.CartItems.Any()) return;
        
        _db.CartItems.RemoveRange(cart.CartItems);
        await _db.SaveChangesAsync();
    }

    private async Task<Models.Cart?> GetCartFromUserId(Guid userId, bool trackChanges = false)
    {
        var query = _db.Carts.AsQueryable();

        if (!trackChanges)
        {
            query = query.AsNoTracking();
        }
        
        var cart = await query
            .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.Category)
            .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Discount)
            .FirstOrDefaultAsync(x => x.UserId == userId);

        return cart;
    }
}