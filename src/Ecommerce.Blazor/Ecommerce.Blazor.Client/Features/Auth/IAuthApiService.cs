using Ecommerce.Shared.DTOs.Auth.Responses;

namespace Ecommerce.Blazor.Client.Features.Auth;

public interface IAuthApiService
{
    Task RegisterAsync(string username, string email, string password);
    Task<LoginResponse> LoginAsync(string email, string password);
    Task<RefreshResponse> RefreshAsync(string refreshToken);
}