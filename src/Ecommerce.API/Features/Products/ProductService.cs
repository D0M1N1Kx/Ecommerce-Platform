using Ecommerce.API.Data;
using Ecommerce.API.Mappings;
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

        return product.MapToResponse()!;
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
}