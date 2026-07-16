using Ecommerce.API.Models;
using Ecommerce.Shared.DTOs.Cart.Responses;
using Ecommerce.Shared.DTOs.Category.Responses;
using Ecommerce.Shared.DTOs.Discount.Responses;
using Ecommerce.Shared.DTOs.Products.Responses;

namespace Ecommerce.API.Mappings;

public static class MappingExtension
{
    public static CategoryResponse? MapToResponse(this Category? category)
    {
        if (category is null) return null;

        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
    }

    public static DiscountResponse? MapToResponse(this Discount? discount)
    {
        if (discount is null) return null;

        return new DiscountResponse
        {
            Id = discount.Id,
            Name = discount.Name,
            DiscountPrecentage = discount.DiscountPercentage,
            IsActive = discount.IsActive,
            ValidUntil = discount.ValidUntil
        };
    }

    public static ProductResponse? MapToResponse(this Product? product)
    {
        if (product is null) return null;

        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            Sku = product.Sku,
            Category = product.Category.MapToResponse() ?? new CategoryResponse(),
            Discount = product.Discount.MapToResponse()
        };
    }

    public static CartItemResponse? MapToResponse(this CartItem? cartItem)
    {
        if (cartItem is null) return null;

        return new CartItemResponse
        {
            Id = cartItem.Id,
            Product = cartItem.Product.MapToResponse(),
            Quantity = cartItem.Quantity
        };
    }

    public static CartResponse? MapToResponse(this Cart? cart)
    {
        if (cart is null) return null;

        return new CartResponse
        {
            Id = cart.Id,
            CartItems = cart.CartItems?
                .Select(x => x.MapToResponse())
                .Where(x => x != null)
                .ToList()
        };
    }
}