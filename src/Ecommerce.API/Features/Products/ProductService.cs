using Ecommerce.Shared.DTOs.Products.Requests;
using Ecommerce.Shared.DTOs.Products.Responses;

namespace Ecommerce.API.Features.Products;

public class ProductService : IProductService
{
    public async Task<List<ProductResponse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ProductResponse> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task CreateAsync(CreateProductRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(UpdateProductRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}