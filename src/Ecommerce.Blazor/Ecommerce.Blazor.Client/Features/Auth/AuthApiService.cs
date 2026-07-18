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
        throw new NotImplementedException();
    }

    public async Task<LoginResponse> LoginAsync(string email, string password)
    {
        throw new NotImplementedException();
    }
}