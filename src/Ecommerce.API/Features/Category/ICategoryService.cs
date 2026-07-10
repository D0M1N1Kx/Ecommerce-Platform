using Ecommerce.Shared.DTOs.Category.Responses;

namespace Ecommerce.API.Features.Category;

public interface ICategoryService
{
    Task<List<CategoryResponse>> GetAllCategories();

    Task CreateCategory(string name, string description);
    
    CategoryResponse MapToResponse(Models.Category c);
}