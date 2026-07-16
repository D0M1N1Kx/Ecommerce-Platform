using Ecommerce.Shared.DTOs.Products.Responses;

namespace Ecommerce.Shared.DTOs.Cart.Responses;

public class CartItemResponse
{
    public int Id { get; set; }
    public ProductResponse? Product { get; set; } = new ProductResponse();
    public int Quantity { get; set; }
}