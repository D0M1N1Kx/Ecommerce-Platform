using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Shared.DTOs.Discount.Requests;

public class UpdateDiscountRequest
{
    [Required]
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string? Name { get; set; } = string.Empty;
    
    [Range(1.00, 100.00)]
    public decimal? DiscountPercentage { get; set; }
    
    public DateTime? ValidUntil { get; set; }
}