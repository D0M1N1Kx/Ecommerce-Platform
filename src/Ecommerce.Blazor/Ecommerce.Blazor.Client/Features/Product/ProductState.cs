using Ecommerce.Shared.DTOs.Products.Responses;

namespace Ecommerce.Blazor.Client.Features.Product;

public class ProductState
{
    private readonly IProductApiService _productApi;

    public event Action? OnChange;
    public List<ProductResponse> Products { get; private set; } = [];

    public ProductState(IProductApiService productApi)
    {
        _productApi = productApi;
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}