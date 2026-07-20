using System.Net.Http.Json;
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
        var response = await _http.GetAsync("discount");

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(error);
        }

        var result = await response.Content.ReadFromJsonAsync<List<DiscountResponse>>()
                     ?? throw new InvalidOperationException("Empty response from server");

        return result;
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