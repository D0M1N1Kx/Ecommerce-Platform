using Ecommerce.Shared.DTOs.Category.Responses;

namespace Ecommerce.Blazor.Client.Features.Category;

public class CategoryState
{
    private readonly ICategoryApiService _categoryApi;
    
    public event Action? OnChange;
    public List<CategoryResponse> Categories { get; private set; } = [];

    public CategoryState(ICategoryApiService categoryApi)
    {
        _categoryApi = categoryApi;
    }

    public async Task LoadCategoriesAsync()
    {
        Categories = await _categoryApi.GetAllAsync();
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}