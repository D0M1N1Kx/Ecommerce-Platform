using Ecommerce.Shared.DTOs.Discount.Responses;

namespace Ecommerce.API.Features.Discount;

public interface IDiscountService
{
    Task<List<DiscountResponse>> GetAllDiscounts();

    Task CreateDiscount(string name, decimal percentage, DateTime? validUntil);
    
    Task UpdateDiscount(int id, string? name, decimal? percentage, DateTime? validUntil);
    
    private static DiscountResponse MapToResponse(Models.Discount d)
    {
        throw new NotImplementedException();
    }
}