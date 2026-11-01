using System.Net.Http.Headers;
using System.Net.Http.Json;
using Ecommerce.Shared.DTOs.Category.Responses;
using Microsoft.AspNetCore.Components.Forms;

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

    public async Task CreateAsync(string name, string description, IBrowserFile imageFile)
    {
        using var content = new MultipartFormDataContent();
        
        content.Add(new StringContent(name), "Name");
        content.Add(new StringContent(description), "Description");

        var maxAllowedSize = 5 * 1024 * 1024; // 5 MB
        var fileStream = imageFile.OpenReadStream(maxAllowedSize);
        var fileContent = new StreamContent(fileStream);

        fileContent.Headers.ContentType = new MediaTypeHeaderValue(imageFile.ContentType);
        
        content.Add(fileContent, "image", imageFile.Name);

        var response = await _http.PostAsJsonAsync("category", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Failed to create category: {error}");
        }
    }
}