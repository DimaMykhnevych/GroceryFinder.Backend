namespace GroceryFinder.DataLayer.Models;

public class ProductGroceryStore
{
    public Guid Id { get; set; }
    public Guid GroceryStoreId { get; set; }
    public Guid ProductId { get; set; }
    public double Price { get; set; }

    public GroceryStore GroceryStore { get; set; }
    public Product Product { get; set; }
}

