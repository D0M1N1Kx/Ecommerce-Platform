using Ecommerce.Shared.DTOs.Cart.Responses;

namespace Ecommerce.Blazor.Client.Features.Cart;

public interface ICartApiService
{
    Task<CartResponse> GetAsync();
    Task AddItemAsync(Guid productId, int quantity = 1);
    Task ChangeQuantityAsync(Guid productId, int amount, bool isIncrement);
    Task DeleteItemAsync(Guid productId);
    Task ClearCartAsync();
}