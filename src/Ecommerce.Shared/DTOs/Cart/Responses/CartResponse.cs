namespace Ecommerce.Shared.DTOs.Cart.Responses;

public class CartResponse
{
    public Guid Id { get; set; }
    public List<CartItemResponse?>? CartItems { get; set; } = [];
}