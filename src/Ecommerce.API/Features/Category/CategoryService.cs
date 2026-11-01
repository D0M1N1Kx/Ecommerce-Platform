using System.Data;
using Ecommerce.API.Data;
using Ecommerce.API.Mappings;
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
            .Select(c => c.MapToResponse())
            .ToList();
        
        return categories;
    }

    public async Task CreateCategory(string name, string description, IFormFile image)
    {
        var isCategoryExists = await _db.Categories.AnyAsync(x => x.Name == name);
        if (isCategoryExists)
            throw new DuplicateNameException("Category with that name already exists");

        string imagePath = null;

        if (image != null && image.Length > 0)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "categories");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(image.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            
            imagePath = $"/images/categories/{uniqueFileName}";
        }
        
        var category = new Models.Category
        {
            Name = name,
            Description = description,
            ImgUrl = imagePath
        };
        
        _db.Categories.Add(category);
        await _db.SaveChangesAsync();
    }
}