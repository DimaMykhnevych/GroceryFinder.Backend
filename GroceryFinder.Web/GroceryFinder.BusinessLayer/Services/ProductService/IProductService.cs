using GroceryFinder.BusinessLayer.DTOs;

namespace GroceryFinder.BusinessLayer.Services.ProductService;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProducts();
    Task<ProductDto> AddProduct(ProductDto productDto);
}

