using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Shared.DTOs.Products.Requests;

public class UpdateProductRequest
{
    [Required]
    public Guid ProductId { get; set; }
    
    [MaxLength(150)]
    public string? Name { get; set; } = string.Empty;
    
    public string? Description { get; set; } = string.Empty;
    
    public decimal? Price { get; set; }
    
    public int? Stock { get; set; }
    
    [MaxLength(50)]
    public string? Sku { get; set; } = string.Empty;
    
    public int? CategoryId { get; set; }
    
    public int? DiscountId { get; set; }
}