using System.Net.Http.Json;
using Ecommerce.Shared.DTOs.Category.Responses;

namespace Ecommerce.Blazor.Client.Features.Category;

public class CategoryApiService : ICategoryApiService
{
    private readonly HttpClient _http;

    public CategoryApiService(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<List<CategoryResponse>> GetAllAsync()
    {
        var response = await _http.GetAsync("category");

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(error);
        }

        var result = await response.Content.ReadFromJsonAsync<List<CategoryResponse>>()
                     ?? throw new InvalidOperationException("Empty response from server");

        return result;
    }

    public async Task CreateAsync(string name, string description)
    {
        throw new NotImplementedException();
    }
}