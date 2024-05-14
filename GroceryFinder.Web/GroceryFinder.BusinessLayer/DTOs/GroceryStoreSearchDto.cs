namespace GroceryFinder.BusinessLayer.DTOs;

public class GroceryStoreSearchDto
{
    public Guid ProductId { get; set; }
    public GroceryStoreSearchModeDto? GroceryStoreSearchMode { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double? Radius { get; set; }
}
