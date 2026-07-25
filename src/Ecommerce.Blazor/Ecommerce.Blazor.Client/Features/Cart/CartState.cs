using Ecommerce.Shared.DTOs.Cart.Responses;

namespace Ecommerce.Blazor.Client.Features.Cart;

public class CartState
{
    private readonly ICartApiService _cartApi;

    public event Action? OnChange;
    public CartResponse? Cart { get; private set; } = new CartResponse();

    public CartState(ICartApiService cartApi)
    {
        _cartApi = cartApi;
    }

    public async Task GetAsync()
    {
        Cart = await _cartApi.GetAsync();
        NotifyStateChanged();
    }

    public async Task AddItemAsync(Guid productId, int quantity = 1)
    {
        await _cartApi.AddItemAsync(productId, quantity);
        await GetAsync();
    }

    public async Task ChangeQuantityAsync(Guid productId, int amount, bool isIncrement)
    {
        await _cartApi.ChangeQuantityAsync(productId, amount, isIncrement);
        await GetAsync();
    }

    public async Task DeleteItemAsync(Guid productId)
    {
        await _cartApi.DeleteItemAsync(productId);
        await GetAsync();
    }

    public async Task ClearCartAsync()
    {
        await _cartApi.ClearCartAsync();
        await GetAsync();
    }
    
    private void NotifyStateChanged() => OnChange?.Invoke();
}