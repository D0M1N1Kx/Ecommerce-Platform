using Ecommerce.API.Models;

namespace Ecommerce.API.Shared.Services;

public interface ITokenService
{
    public string GenerateAccessToken(User user);
    
    public string GenerateRefreshToken();
    
    public string HashToken(string token);
}