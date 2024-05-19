using GroceryFinder.BusinessLayer.DTOs;

namespace GroceryFinder.BusinessLayer.Services.ProductGroceryStoreService;

public interface IProductGroceryStoreService
{
    Task<IEnumerable<ProductGroceryStoreDto>> GetAllProductGroceryStores();
    Task<ProductGroceryStoreDto> AddProductGroceryStore(ProductGroceryStoreDto productGroceryStoreDto);
    Task<ProductGroceryStoreDto> UpdateProductGroceryStore(ProductGroceryStoreDto productGroceryStoreDto);
}

