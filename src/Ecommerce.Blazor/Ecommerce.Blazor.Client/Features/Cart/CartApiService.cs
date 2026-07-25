using System.Net.Http.Json;
using Ecommerce.Shared.DTOs.Cart.Requests;
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
        var response = await _http.GetAsync("cart");

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(error);
        }

        var result = await response.Content.ReadFromJsonAsync<CartResponse>()
            ?? throw new InvalidOperationException("Empty response from server");

        return result;
    }

    public async Task AddItemAsync(Guid productId, int quantity = 1)
    {
        var request = new AddCartItemRequest
        {
            ProductId = productId,
            Quantity = quantity
        };

        var response = await _http.PostAsJsonAsync("cart/items", request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(error);
        }
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