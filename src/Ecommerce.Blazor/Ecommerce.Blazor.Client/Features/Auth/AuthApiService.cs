using System.Net.Http.Json;
using Ecommerce.Shared.DTOs.Auth.Requests;
using Ecommerce.Shared.DTOs.Auth.Responses;

namespace Ecommerce.Blazor.Client.Features.Auth;

public class AuthApiService : IAuthApiService
{
    private readonly HttpClient _http;

    public AuthApiService(HttpClient http)
    {
        _http = http;
    }
    
    public async Task RegisterAsync(string username, string email, string password)
    {
        var request = new RegisterRequest
        {
            Username = username,
            Email = email,
            Password = password
        };

        var response = await _http.PostAsJsonAsync("auth/register", request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Registration failed: {error}");
        }
    }

    public async Task<LoginResponse> LoginAsync(string email, string password)
    {
        throw new NotImplementedException();
    }
}