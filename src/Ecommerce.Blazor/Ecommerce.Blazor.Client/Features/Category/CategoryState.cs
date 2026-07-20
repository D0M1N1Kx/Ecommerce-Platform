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

    public async Task CreateCategoryAsync(string name, string description)
    {
        await _categoryApi.CreateAsync(name, description);
        await LoadCategoriesAsync();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}