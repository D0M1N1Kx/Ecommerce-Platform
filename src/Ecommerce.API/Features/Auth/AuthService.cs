using Ecommerce.API.Data;

namespace Ecommerce.API.Features.Auth;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;

    public AuthService(AppDbContext db)
    {
        _db = db;
    }
    
    public Task<bool> RegisterAsync(string username, string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<string?> LoginAsync(string email, string password)
    {
        throw new NotImplementedException();
    }
}