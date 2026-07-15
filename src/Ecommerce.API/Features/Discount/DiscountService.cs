using System.Data;
using Ecommerce.API.Data;
using Ecommerce.API.Mappings;
using Ecommerce.Shared.DTOs.Discount.Responses;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Features.Discount;

public class DiscountService : IDiscountService
{
    private readonly AppDbContext _db;
    
    public DiscountService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<DiscountResponse>> GetAllDiscounts()
    {
        var rawDiscounts = await _db.Discounts
            .AsNoTracking()
            .ToListAsync();
        
        if (rawDiscounts.Count == 0)
            throw new KeyNotFoundException("No discounts found");
        
        var discounts = rawDiscounts
            .Select(d => d.MapToResponse()!)
            .ToList();
        
        return discounts;
    }

    public async Task CreateDiscount(string name, decimal percentage, DateTime? validUntil)
    {
        var existing = await _db.Discounts.FirstOrDefaultAsync(x => x.Name == name);
        if (existing != null)
            throw new DuplicateNameException($"Discount with name {name} already exists");

        var discount = new Models.Discount
        {
            Name = name,
            DiscountPercentage = percentage,
            ValidUntil = validUntil
        };
        
        await _db.Discounts.AddAsync(discount);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateDiscount(int id, string? name, decimal? percentage, DateTime? validUntil)
    {
        var discount = await _db.Discounts.FirstOrDefaultAsync(x => x.Id == id);
        if (discount is null)
            throw new KeyNotFoundException($"Discount with name {name} not found");
        
        if (name != null) discount.Name = name;
        if (percentage != null) discount.DiscountPercentage = percentage.Value;
        if (validUntil != null) discount.ValidUntil = validUntil;
        
        await _db.SaveChangesAsync();
    }
}