using Ecommerce.Shared.DTOs.Products.Responses;

namespace Ecommerce.Blazor.Client.Features.Product;

public interface IProductApiService
{
    Task<List<ProductResponse>> GetAllAsync(
        int page,
        int pageSize,
        string? searchTerm, 
        int? categoryId, 
        decimal? minPrice, 
        decimal? maxPrice, 
        bool? inStockOnly);

    Task<ProductResponse> GetByIdAsync(Guid id);

    Task CreateAsync(
        string name,
        string description,
        decimal price,
        int stock,
        string sku,
        int categoryId,
        int? discountId);
    
    Task UpdateAsync(
        Guid id,
        string? name,
        string? description,
        decimal? price,
        int? stock,
        string? sku,
        int? categoryId,
        int? discountId);

    Task DeleteAsync(Guid id);
}