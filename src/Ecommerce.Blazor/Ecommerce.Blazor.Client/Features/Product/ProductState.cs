using Ecommerce.Shared.DTOs.Products.Responses;

namespace Ecommerce.Blazor.Client.Features.Product;

public class ProductState
{
    private readonly IProductApiService _productApi;

    public event Action? OnChange;
    public List<ProductResponse> Products { get; private set; } = [];
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public ProductState(IProductApiService productApi)
    {
        _productApi = productApi;
    }

    public async Task LoadProductsAsync(string? searchTerm = null, int? categoryId = null, decimal? minPrice = null,
        decimal? maxPrice = null, bool? inStockOnly = null)
    {
        Products = await _productApi.GetAllAsync(Page, PageSize, searchTerm, categoryId, minPrice, maxPrice, inStockOnly);
        NotifyStateChanged();
    }

    public async Task<ProductResponse> GetByIdAsync(Guid id)
    {
        var product = await _productApi.GetByIdAsync(id);
        return product;
    }

    public async Task CreateAsync(string name, string description, decimal price,
        int stock, string sku, int categoryId, int? discountId)
    {
        await _productApi.CreateAsync(name, description, price, stock, sku, categoryId, discountId);
        await LoadProductsAsync();
    }

    public async Task UpdateAsync(Guid id, string? name, string? description, decimal? price, int? stock, string? sku,
        int? categoryId, int? discountId)
    {
        await _productApi.UpdateAsync(id, name, description, price, stock, sku, categoryId, discountId);
        await LoadProductsAsync();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}