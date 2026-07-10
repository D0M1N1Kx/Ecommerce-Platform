using System.Data;
using Ecommerce.API.Data;
using Ecommerce.API.Models;

namespace Ecommerce.API.Features.Auth;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;

    public AuthService(AppDbContext db)
    {
        _db = db;
    }
    
    public async Task RegisterAsync(string username, string email, string password)
    {
        var existingUser = _db.Users.FirstOrDefault(x => x.Username == username && x.Email == email);
        if (existingUser != null)
            throw new DuplicateNameException($"Username or email is already registered.");
        
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        var user = new User
        {
            Username = username,
            Email = email,
            PasswordHash = hashedPassword
        };
        
        _db.Users.Add(user);

        var cart = new Cart
        {
            UserId = user.Id,
        };
        
        _db.Carts.Add(cart);
        
        await _db.SaveChangesAsync();
    }

    public Task<string?> LoginAsync(string email, string password)
    {
        throw new NotImplementedException();
    }
}