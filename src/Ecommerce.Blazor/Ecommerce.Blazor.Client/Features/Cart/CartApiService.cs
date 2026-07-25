using Ecommerce.Shared.DTOs.Cart.Responses;

namespace Ecommerce.Blazor.Client.Features.Cart;

public class CartApiService : ICartApiService
{
    private readonly HttpClient _http;

    public CartApiService(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<CartResponse> GetAsync()
    {
        throw new NotImplementedException();
    }

    public async Task AddItemAsync(Guid productId, int quantity = 1)
    {
        throw new NotImplementedException();
    }

    public async Task ChangeQuantityAsync(Guid productId, int amount, bool isIncrement)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteItemAsync(Guid productId)
    {
        throw new NotImplementedException();
    }

    public async Task ClearCartAsync()
    {
        throw new NotImplementedException();
    }
}