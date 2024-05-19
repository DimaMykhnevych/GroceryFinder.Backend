using GroceryFinder.DataLayer.Models;

namespace GroceryFinder.BusinessLayer.DTOs;

public class PriceUpdateEmailInfoDto
{
    public AppUser Receiver { get; set; }
    public GroceryStoreDto GroceryStore { get; set; }
    public ProductDto Product { get; set; }
    public double OldPrice { get; set; }
    public double NewPrice { get; set; }
}