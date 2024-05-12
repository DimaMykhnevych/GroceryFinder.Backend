namespace GroceryFinder.BusinessLayer.DTOs;

public class ProductGroceryStoreDto
{
    public Guid Id { get; set; }
    public Guid GroceryStoreId { get; set; }
    public Guid ProductId { get; set; }
    public double Price { get; set; }
}

