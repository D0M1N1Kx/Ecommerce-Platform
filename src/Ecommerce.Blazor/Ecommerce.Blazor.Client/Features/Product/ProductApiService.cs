using System.Net.Http.Json;
using Ecommerce.Shared.DTOs.Products.Responses;
using Microsoft.AspNetCore.WebUtilities;

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
        var queryParams = new Dictionary<string, string?>();

        if (!string.IsNullOrWhiteSpace(searchTerm))
            queryParams["SearchTerm"] = searchTerm;

        if (categoryId.HasValue)
            queryParams["CategoryId"] = categoryId.ToString();

        if (minPrice.HasValue)
            queryParams["MinPrice"] = minPrice.ToString();

        if (maxPrice.HasValue)
            queryParams["MaxPrice"] = maxPrice.ToString();

        if (inStockOnly.HasValue)
            queryParams["InStockOnly"] = inStockOnly.ToString();

        queryParams["PageNumber"] = page.ToString();
        queryParams["PageSize"] = pageSize.ToString();

        var url = QueryHelpers.AddQueryString("products", queryParams);

        var response = await _http.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(error);
        }

        var result = await response.Content.ReadFromJsonAsync<List<ProductResponse>>() 
                     ?? throw new InvalidOperationException("Empty response from server");

        return result;
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