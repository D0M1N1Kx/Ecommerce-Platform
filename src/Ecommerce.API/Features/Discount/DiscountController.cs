using Ecommerce.Shared.DTOs.Discount.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Features.Discount;

[ApiController]
[Route("/discount")]
[Authorize(Roles = "Admin")]
public class DiscountController : ControllerBase
{
    private readonly IDiscountService _discountService;
    
    public DiscountController(IDiscountService discountService)
    {
        _discountService = discountService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDiscounts()
    {
        var discounts = await _discountService.GetAllDiscounts();
        return Ok(discounts);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDiscount([FromBody] CreateDiscountRequest request)
    {
        await _discountService.CreateDiscount(request.Name, request.DiscountPercentage, request.ValidUntil);
        return Created();
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateDiscount([FromBody] UpdateDiscountRequest request)
    {
        await _discountService.UpdateDiscount(request.Id, request.Name, request.DiscountPercentage, request.ValidUntil);
        return Ok();
    }
}