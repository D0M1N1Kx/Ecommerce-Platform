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
    
    private void NotifyStateChanged() => OnChange?.Invoke();
}