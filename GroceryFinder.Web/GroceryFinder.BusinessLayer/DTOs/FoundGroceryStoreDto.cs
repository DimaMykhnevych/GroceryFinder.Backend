namespace GroceryFinder.BusinessLayer.DTOs;

public class FoundGroceryStoreDto
{
    public ProductDto Product { get; set; }
    public IEnumerable<FoundProductGroceryStoreDto> FoundStores { get; set; }
}

