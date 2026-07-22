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

    public async Task LoadProductsAsync(string? searchTerm, int? categoryId, decimal? minPrice,
        decimal? maxPrice, bool? inStockOnly, int page = 1, int pageSize = 10)
    {
        Products = await _productApi.GetAllAsync(page, pageSize, searchTerm, categoryId, minPrice, maxPrice, inStockOnly);
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}