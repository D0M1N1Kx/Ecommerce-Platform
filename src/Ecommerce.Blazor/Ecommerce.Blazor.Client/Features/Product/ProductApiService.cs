using Ecommerce.Shared.DTOs.Products.Responses;

namespace Ecommerce.Blazor.Client.Features.Product;

public class ProductApiService : IProductApiService
{
    private readonly HttpClient _http;

    public ProductApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ProductResponse>> GetAllAsync(int page, int pageSize, string? searchTerm, int? categoryId, decimal? minPrice, decimal? maxPrice,
        bool? inStockOnly)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductResponse> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task CreateAsync(string name, string description, decimal price, int stock, string sku, int categoryId,
        int? discountId)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Guid id, string? name, string? description, decimal? price, int? stock, string? sku, int? categoryId,
        int? discountId)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}