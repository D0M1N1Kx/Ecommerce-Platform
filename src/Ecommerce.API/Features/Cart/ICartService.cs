using Ecommerce.Shared.DTOs.Cart.Requests;
using Ecommerce.Shared.DTOs.Cart.Responses;

namespace Ecommerce.API.Features.Cart;

public interface ICartService
{
    Task<CartResponse> GetAsync(Guid userId);

    Task AddCartItemAsync(Guid userId, AddCartItemRequest request);

    Task ChangeQuantityAsync(Guid userId, ChangeCartItemQuantityRequest request);

    Task DeleteCartItemAsync(Guid userId, Guid productId);

    Task ClearCartAsync(Guid userId);
}