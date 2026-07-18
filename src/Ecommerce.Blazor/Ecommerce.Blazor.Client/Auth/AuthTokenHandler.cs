using Ecommerce.Blazor.Client.Features.Auth;

namespace Ecommerce.Blazor.Client.Auth;

public class AuthTokenHandler : DelegatingHandler
{
    private readonly AuthState _authState;

    public AuthTokenHandler(AuthState authState) => _authState = authState;
}