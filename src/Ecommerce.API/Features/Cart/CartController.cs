using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Features.Cart;

[ApiController]
[Route("/cart")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }
}