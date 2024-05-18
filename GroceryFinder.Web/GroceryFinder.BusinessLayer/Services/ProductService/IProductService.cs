using GroceryFinder.BusinessLayer.DTOs;

namespace GroceryFinder.BusinessLayer.Services.ProductService;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProducts(Guid? userId);
    Task<ProductDto> GetProduct(Guid id);
    Task<ProductDto> AddProduct(ProductDto productDto);
}

