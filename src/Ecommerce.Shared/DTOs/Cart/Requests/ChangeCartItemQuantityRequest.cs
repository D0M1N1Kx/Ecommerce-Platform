namespace Ecommerce.Shared.DTOs.Cart.Requests;

public class ChangeCartItemQuantityRequest
{
    public Guid ProductId { get; set; }
    public int Amount { get; set; }
    public bool IsIncrement { get; set; }
}