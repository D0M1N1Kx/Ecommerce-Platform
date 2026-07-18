using System.Net;
using Ecommerce.Blazor.Client.Features.Auth;

namespace Ecommerce.Blazor.Client.Auth;

public class AuthTokenHandler : DelegatingHandler
{
    private readonly AuthState _authState;
    
    public AuthTokenHandler(AuthState authState) => _authState = authState;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
    {
        var token = await _authState.GetAccessTokenAsync();
        if (!string.IsNullOrEmpty(token))
            request.Headers.Authorization = new("Bearer", token);

        var response = await base.SendAsync(request, ct);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var refreshed = await _authState.TryRefreshTokenAsync();
            if (refreshed)
            {
                request.Headers.Authorization = new("Bearer", await _authState.GetAccessTokenAsync());
                response = await base.SendAsync(request, ct);
            }
        }

        return response;
    }
}