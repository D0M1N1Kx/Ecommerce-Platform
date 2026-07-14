namespace Ecommerce.Shared.DTOs.Discount.Responses;

public class DiscountResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal DiscountPrecentage { get; set; }
    public bool IsActive { get; set; }
    public DateTime? ValidUntil { get; set; }
}