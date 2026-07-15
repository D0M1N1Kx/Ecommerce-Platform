using Ecommerce.API.Data;
using Ecommerce.API.Mappings;
using Ecommerce.API.Models;
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
        var category = await _db.Categories
            .FirstOrDefaultAsync(x => x.Id == request.CategoryId)
            ?? throw new KeyNotFoundException($"Category not found with id: {request.CategoryId}");

        var discount = request.DiscountId is null
            ? null
            : await _db.Discounts
                  .FirstOrDefaultAsync(x => x.Id == request.DiscountId)
              ?? throw new KeyNotFoundException($"Discount not found with id: {request.DiscountId}");
        
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Stock = request.Stock,
            Sku = request.Sku,
            CategoryId = category.Id,
            DiscountId = discount?.Id
        };

        _db.Products.Add(product);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(UpdateProductRequest request)
    {
        var product = await _db.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId)
            ?? throw new KeyNotFoundException($"Product not found with id: {request.ProductId}");

        if (request.CategoryId != null)
        {
            var categoryExists = await _db.Categories
                .AnyAsync(x => x.Id == request.CategoryId);
            if (!categoryExists)
                throw new KeyNotFoundException($"Category not found with id: {request.CategoryId}");

            product.CategoryId = request.CategoryId.Value;
        }

        if (request.DiscountId != null)
        {
            var discountExists = await _db.Discounts
                .AnyAsync(x => x.Id == request.DiscountId);
            if (!discountExists)
                throw new KeyNotFoundException($"Discount not found with id: {request.DiscountId}");

            product.DiscountId = request.DiscountId.Value;
        }
        
        if (request.Name != null) product.Name = request.Name;
        if (request.Description != null) product.Description = request.Description;
        if (request.Price != null) product.Price = (decimal)request.Price;
        if (request.Stock != null) product.Stock = (int)request.Stock;
        if (request.Sku != null) product.Sku = request.Sku;

        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}