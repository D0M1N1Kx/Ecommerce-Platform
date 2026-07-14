using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Shared.DTOs.Discount.Requests;

public class CreateDiscountRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [Range(1.00, 100.00)]
    public decimal DiscountPercentage { get; set; }
    
    [Required]
    public DateTime? ValidUntil { get; set; }
}