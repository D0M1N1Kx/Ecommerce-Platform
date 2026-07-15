using Ecommerce.Shared.DTOs.Products.Requests;
using Ecommerce.Shared.DTOs.Products.Responses;

namespace Ecommerce.API.Features.Products;

public interface IProductService
{
    Task<List<ProductResponse>> GetAllAsync(GetProductsRequest request);
    
    Task<ProductResponse> GetByIdAsync(Guid id);
    
    Task CreateAsync(CreateProductRequest request);
    
    Task UpdateAsync(UpdateProductRequest request);
    
    Task DeleteAsync(Guid id);
    
    private static ProductResponse MapToResponse(Models.Product p)
    {
        throw new NotImplementedException();
    }
}