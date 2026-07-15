using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Shared.DTOs.Products.Requests;

public class CreateProductRequest
{
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(255)]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    public decimal Price { get; set; }

    [Required] 
    public int Stock { get; set; } = 0;
    
    [Required]
    [MaxLength(50)]
    public string Sku { get; set; } = string.Empty;
    
    [Required]
    public int CategoryId { get; set; }
    
    public int? DiscountId { get; set; }
}