using Ecommerce.Shared.DTOs.Category.Responses;

namespace Ecommerce.Blazor.Client.Features.Category;

public interface ICategoryApiService
{
    Task<List<CategoryResponse>> GetAllAsync();
    Task CreateAsync(string name, string description);
}