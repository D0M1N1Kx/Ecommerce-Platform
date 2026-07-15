using Ecommerce.API.Data;
using Ecommerce.Shared.DTOs.Category.Responses;
using Ecommerce.Shared.DTOs.Discount.Responses;
using Ecommerce.Shared.DTOs.Products.Requests;
using Ecommerce.Shared.DTOs.Products.Responses;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Features.Products;

public class ProductService : IProductService
{
    private readonly AppDbContext _db;

    public ProductService(AppDbContext db)
    {
        _db = db;
    }
    
    public async Task<List<ProductResponse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ProductResponse> GetByIdAsync(Guid id)
    {
        var product = await _db.Products
            .Include(x => x.Category)
            .Include(x => x.Discount)
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new KeyNotFoundException($"Product not found with id: {id}");

        return MapToResponse(product);
    }

    public async Task CreateAsync(CreateProductRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(UpdateProductRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    private static ProductResponse MapToResponse(Models.Product p) => new()
    {
        Id = p.Id,
        Name = p.Name,
        Description = p.Description,
        Price = p.Price,
        Stock = p.Stock,
        Sku = p.Sku,
        Category = MapToResponse(p.Category),
        Discount = p.Discount != null ? MapToResponse(p.Discount) : null
    };
    
    private static CategoryResponse MapToResponse(Models.Category c) => new()
    {
        Id = c.Id,
        Name = c.Name,
        Description = c.Description
    };
    
    private static DiscountResponse MapToResponse(Models.Discount d) => new()
    {
        Id = d.Id,
        Name = d.Name,
        DiscountPrecentage = d.DiscountPercentage,
        IsActive = d.IsActive,
        ValidUntil = d.ValidUntil
    };
}