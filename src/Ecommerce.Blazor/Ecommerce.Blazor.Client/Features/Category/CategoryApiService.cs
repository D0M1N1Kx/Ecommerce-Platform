using Ecommerce.Shared.DTOs.Category.Responses;

namespace Ecommerce.Blazor.Client.Features.Category;

public class CategoryApiService : ICategoryApiService
{
    public async Task<List<CategoryResponse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task CreateAsync(string name, string description)
    {
        throw new NotImplementedException();
    }
}