using Ecommerce.Shared.DTOs.Category.Responses;
using Microsoft.AspNetCore.Components.Forms;

namespace Ecommerce.Blazor.Client.Features.Category;

public interface ICategoryApiService
{
    Task<List<CategoryResponse>> GetAllAsync();
    Task CreateAsync(string name, string description, IBrowserFile imageFile);
}