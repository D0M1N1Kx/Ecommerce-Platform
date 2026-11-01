using Ecommerce.Shared.DTOs.Category.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Features.Category;

[ApiController]
[Route("/category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetAllCategories();
        return Ok(categories);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateCategory([FromForm] CreateCategory request, IFormFile image)
    {
        await _categoryService.CreateCategory(request.Name, request.Description, image);
        return Created();
    }
}