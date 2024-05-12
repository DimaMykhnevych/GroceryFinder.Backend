using GroceryFinder.BusinessLayer.DTOs;

namespace GroceryFinder.BusinessLayer.Services.GroceryStoreService;

public interface IGroceryStoreService
{
    Task<IEnumerable<GroceryStoreDto>> GetAllGroceryStores();
    Task<GroceryStoreDto> AddGroceryStore(GroceryStoreDto groceryStoreDto);
}

