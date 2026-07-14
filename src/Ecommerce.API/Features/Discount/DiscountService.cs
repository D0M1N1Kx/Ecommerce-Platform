using Ecommerce.API.Data;

namespace Ecommerce.API.Features.Discount;

public class DiscountService : IDiscountService
{
    private readonly AppDbContext _db;
    
    public DiscountService(AppDbContext db)
    {
        _db = db;
    }

    public Task GetAllDiscounts()
    {
        throw new NotImplementedException();
    }

    public Task CreateDiscount(string name, decimal percentage, DateTime validUntil)
    {
        throw new NotImplementedException();
    }

    public Task UpdateDiscount(int id, string name, decimal percentage, DateTime validUntil)
    {
        throw new NotImplementedException();
    }
}