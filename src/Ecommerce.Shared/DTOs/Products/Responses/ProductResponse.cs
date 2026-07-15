using Ecommerce.Shared.DTOs.Category.Responses;
using Ecommerce.Shared.DTOs.Discount.Responses;

namespace Ecommerce.Shared.DTOs.Products.Responses;

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; } = 0;
    public string Sku { get; set; } = string.Empty;
    public CategoryResponse? Category { get; set; } = new CategoryResponse();
    public DiscountResponse? Discount { get; set; }
}