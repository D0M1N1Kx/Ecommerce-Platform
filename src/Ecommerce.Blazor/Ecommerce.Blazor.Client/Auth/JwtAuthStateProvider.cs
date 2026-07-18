using System.Security.Claims;
using System.Text.Json;
using Ecommerce.Blazor.Client.Features.Auth;
using Microsoft.AspNetCore.Components.Authorization;

namespace Ecommerce.Blazor.Client.Auth;

public class JwtAuthStateProvider : AuthenticationStateProvider
{
    private readonly AuthState _authState;

    public JwtAuthStateProvider(AuthState authState)
    {
        _authState = authState;
        _authState.OnChange += () => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _authState.GetAccessTokenAsync();
        var identity = string.IsNullOrEmpty(token)
            ? new ClaimsIdentity()
            : new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);

        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes)
                            ?? throw new InvalidOperationException("Failed to parse JWT payload");

        var claims = new List<Claim>();

        foreach (var kvp in keyValuePairs)
        {
            if (kvp.Value is JsonElement { ValueKind: JsonValueKind.Array } element)
            {
                claims.AddRange(element.EnumerateArray()
                    .Select(item => new Claim(MapClaimType(kvp.Key), item.ToString())));
            }
            else
            {
                claims.Add(new Claim(MapClaimType(kvp.Key), kvp.Value.ToString() ?? string.Empty));
            }
        }

        return claims;
    }
    
    private static string MapClaimType(string type) => type switch
    {
        "sub" => ClaimTypes.NameIdentifier,
        "email" => ClaimTypes.Email,
        "name" => ClaimTypes.Name,
        _ => type
    };

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        base64 = base64.Replace('-', '+').Replace('_', '/');

        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }

        return Convert.FromBase64String(base64);
    }
}