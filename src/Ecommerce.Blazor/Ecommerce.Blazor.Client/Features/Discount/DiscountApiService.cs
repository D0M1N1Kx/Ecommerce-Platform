using Ecommerce.Shared.DTOs.Discount.Responses;

namespace Ecommerce.Blazor.Client.Features.Discount;

public class DiscountApiService : IDiscountApiService
{
    private readonly HttpClient _http;

    public DiscountApiService(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<List<DiscountResponse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task CreateAsync(string name, decimal discountPercentage, DateTime? validUntil)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(int id, string? name, decimal? discountPercentage, DateTime? validUntil)
    {
        throw new NotImplementedException();
    }
}