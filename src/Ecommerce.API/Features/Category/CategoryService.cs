using System.Data;
using Ecommerce.API.Data;
using Ecommerce.Shared.DTOs.Category.Responses;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Features.Category;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _db;
    
    public CategoryService(AppDbContext db)
    {
        _db = db;
    }
    
    public async Task<List<CategoryResponse>> GetAllCategories()
    {
        var rawCategories = await _db.Categories
            .AsNoTracking()
            .ToListAsync();
        
        if (rawCategories.Count == 0)
            throw new KeyNotFoundException("Category not found");
        
        var categories = rawCategories
            .Select(MapToResponse)
            .ToList();
        
        return categories;
    }

    public async Task CreateCategory(string name, string description)
    {
        var isCategoryExists = await _db.Categories.AnyAsync(x => x.Name == name);
        if (isCategoryExists)
            throw new DuplicateNameException("Category with that name already exists");

        var category = new Models.Category
        {
            Name = name,
            Description = description
        };
        
        _db.Categories.Add(category);
        await _db.SaveChangesAsync();
    }

    private static CategoryResponse MapToResponse(Models.Category c) => new()
    {
        Id = c.Id,
        Name = c.Name,
        Description = c.Description
    };
}