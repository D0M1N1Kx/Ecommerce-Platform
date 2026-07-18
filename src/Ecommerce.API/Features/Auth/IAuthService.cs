using Ecommerce.Shared.DTOs.Auth.Responses;

namespace Ecommerce.API.Features.Auth;

public interface IAuthService
{
    Task RegisterAsync(string username, string email, string password);
    
    Task<LoginResponse> LoginAsync(string email, string password);

    Task<RefreshResponse> RefreshAsync(string refreshToken);
}