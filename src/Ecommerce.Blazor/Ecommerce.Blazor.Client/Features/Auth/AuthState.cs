using Blazored.LocalStorage;

namespace Ecommerce.Blazor.Client.Features.Auth;

public class AuthState
{
    private readonly ILocalStorageService _localStorage;
    private readonly IAuthApiService _authApi;

    public event Action? OnChange;
    private string? _accessToken;

    public async Task<string?> GetAccessTokenAsync()
        => _accessToken ??= await _localStorage.GetItemAsStringAsync("accessToken");

    public async Task SetTokensAsync(string access, string refresh)
    {
        _accessToken = access;
        await _localStorage.SetItemAsStringAsync("accessToken", access);
        await _localStorage.SetItemAsStringAsync("refreshToken", refresh);
        OnChange?.Invoke();
    }

    public async Task LogoutAsync()
    {
        _accessToken = null;
        await _localStorage.RemoveItemAsync("accessToken");
        await _localStorage.RemoveItemAsync("refreshToken");
        OnChange?.Invoke();
    }
}