using Ecommerce.Shared.DTOs.Cart.Requests;
using Ecommerce.Shared.DTOs.Cart.Responses;

namespace Ecommerce.API.Features.Cart;

public class CartService : ICartService
{
    public async Task<CartResponse> GetAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task AddCartItemAsync(Guid userId, AddCartItemRequest request)
    {
        throw new NotImplementedException();
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
}