using Ecommerce.Shared.DTOs.Discount.Requests;

namespace Ecommerce.API.Features.Discount;

public interface IDiscountService
{
    Task GetAllDiscounts();

    Task CreateDiscount(string name, decimal percentage, DateTime validUntil);
    
    Task UpdateDiscount(int id, string name, decimal percentage, DateTime validUntil);
}