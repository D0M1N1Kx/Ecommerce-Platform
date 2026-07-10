using Ecommerce.API.Models;
using Ecommerce.API.Shared.Classes;

namespace Ecommerce.API.Shared.Services;

public interface ITokenService
{
    public string GenerateAccessToken(User user);
    
    public string GenerateRefreshToken();
    
    public string HashToken(string token);
    
    public UserClaims? GetUserClaims(string token);
}