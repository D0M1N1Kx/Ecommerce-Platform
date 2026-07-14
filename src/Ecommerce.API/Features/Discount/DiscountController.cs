using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Features.Discount;

[ApiController]
[Route("/discount")]
public class DiscountController : ControllerBase
{
    private readonly IDiscountService _discountService;
    
    public DiscountController(IDiscountService discountService)
    {
        _discountService = discountService;
    }
}