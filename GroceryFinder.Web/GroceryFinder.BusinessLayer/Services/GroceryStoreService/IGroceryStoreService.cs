using GroceryFinder.BusinessLayer.DTOs;

namespace GroceryFinder.BusinessLayer.Services.GroceryStoreService;

public interface IGroceryStoreService
{
    Task<IEnumerable<GroceryStoreDto>> GetAllGroceryStores();
    Task<FoundGroceryStoreDto> SearchGroceryStores(GroceryStoreSearchDto groceryStoreSearchDto);
    Task<GroceryStoreDto> AddGroceryStore(GroceryStoreDto groceryStoreDto);
}

