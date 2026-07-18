using Blazored.LocalStorage;

namespace Ecommerce.Blazor.Client.Features.Auth;

public class AuthState
{
    private readonly ILocalStorageService _localStorage;
    private readonly IAuthApiService _authApi;
    public event Action? OnChange;
    private string? _accessToken;

    public AuthState(ILocalStorageService localStorage, IAuthApiService authApi)
    {
        _localStorage = localStorage;
        _authApi = authApi;
    }

    public async Task RegisterAsync(string username, string email, string password)
        => await _authApi.RegisterAsync(username, email, password);

    public async Task LoginAsync(string email, string password)
    {
        var result = await _authApi.LoginAsync(email, password);
        await SetTokensAsync(result.AccessToken, result.RefreshToken);
    }

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

    public async Task<bool> TryRefreshTokenAsync()
    {
        var refreshToken = await _localStorage.GetItemAsStringAsync("refreshToken");
        if (refreshToken is null) return false;

        try
        {
            var result = await _authApi.RefreshAsync(refreshToken);
            await SetTokensAsync(result.AccessToken, refreshToken);
            return true;
        }
        catch
        {
            await LogoutAsync();
            return false;
        }
    }
}