namespace GroceryFinder.DataLayer.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUri { get; set; }
    public AllergenType AllergenType { get; set; } = AllergenType.None;

    public ICollection<ProductGroceryStore> ProductGroceryStores { get; set; }
}

