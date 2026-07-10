using Ecommerce.API.Models;
using Ecommerce.API.Settings;
using Ecommerce.API.Shared.Classes;

namespace Ecommerce.API.Shared.Services;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    
    public TokenService(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    public string GenerateAccessToken(User user)
    {
        throw new NotImplementedException();
    }

    public string GenerateRefreshToken()
    {
        throw new NotImplementedException();
    }

    public string HashToken(string token)
    {
        throw new NotImplementedException();
    }

    public UserClaims? GetUserClaims(string token)
    {
        throw new NotImplementedException();
    }
}