using System.Security.Claims;
using Ecommerce.Shared.DTOs.Cart.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Features.Cart;

[ApiController]
[Route("/cart")]
[Authorize(Roles = "Customer,Admin")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCartAsync()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        
        var cart = await _cartService.GetAsync(userId);

        return Ok(cart);
    }

    [HttpPost("/items")]
    public async Task<IActionResult> AddCartItemAsync([FromBody] AddCartItemRequest request)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _cartService.AddCartItemAsync(userId, request);

        return Created();
    }

    [HttpPatch("/items")]
    public async Task<IActionResult> ChangeQuantityAsync([FromBody] ChangeCartItemQuantityRequest request)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _cartService.ChangeQuantityAsync(userId, request);

        return Ok();
    }

    [HttpDelete("/items/{productId:guid}")]
    public async Task<IActionResult> DeleteCartItemAsync(Guid productId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _cartService.DeleteCartItemAsync(userId, productId);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> ClearCart()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _cartService.ClearCartAsync(userId);

        return NoContent();
    }
}