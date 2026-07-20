using Ecommerce.Shared.DTOs.Discount.Responses;

namespace Ecommerce.Blazor.Client.Features.Discount;

public interface IDiscountService
{
    Task<List<DiscountResponse>> GetAllAsync();
    Task CreateAsync(string name, decimal discountPercentage, DateTime? validUntil);
    Task UpdateAsync(int id, string? name, decimal? discountPercentage, DateTime? validUntil);
}